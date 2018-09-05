using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueRaven.Data;
using BlueRaven.Data.Domain;
using BlueRaven.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace BlueRaven.Svc
{
	public class PostService : BaseService
	{
		public PostService(BRContext context, IHostingEnvironment env, IMemoryCache memoryCache, ILogger<PostService> logger)
			: base(env, memoryCache, logger)
		{
			_context = context;
		}

		private BRContext _context;

		public async Task<(IEnumerable<IPost> list, int pageCount)> GetPagedAsync(string blogId, int page = 1, int pageSize = 10)
		{
			return await Task.Run(() =>
			{
				var cacheKey = string.Format(_cacheKeyPosts, blogId);
				IEnumerable<IPost> posts;
				int pageCount = 0;

				if (!_memoryCache.TryGetValue(_cacheKeyPosts, out posts))
				{
					pageCount = _context.Posts.Count() / pageSize;
					if (0 != _context.Posts.Count() % pageSize)
					{
						pageCount++;
					}

					if (page > pageCount)
					{
						page = pageCount;
					}
					if (page < 1)
					{
						page = 1;
					}

					posts = _context.Posts
						.Where(p => p.BlogId == blogId)
						.Skip((page - 1) * pageSize)
						.Take(pageSize)
						.OrderBy(p => p.PubDate);

					// TODO: Move timespan to config
					_memoryCache.Set(cacheKey, posts, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(120)));
					_logger.LogInformation($"{cacheKey} updated from source");
				}
				else
				{
					_logger.LogInformation($"{cacheKey} retrieved from cache");
				}

				return (posts, pageCount);
			});
		}

		public async Task<IPost> GetBySlugAsync(string blogId, int year, int month, int day, string slug)
		{
			var fullSlug = $"{year}/{month}/{day}/{slug}";
			var cacheKey = string.Format(_cacheKeyPost, blogId, fullSlug);
			IPost post;

			if (!_memoryCache.TryGetValue(_cacheKeyPost, out post))
			{
				post = _context.Posts
					.Single(p => p.Slug == fullSlug);

				if (null == post)
				{
					var pubDate = new DateTime(year, month, day);
					post = _context.Posts
						.Single(p => p.Slug == slug && p.PubDate == pubDate);

					if (null != post)
					{
						await _context.SaveChangesAsync();
						_logger.LogInformation($"Updated {fullSlug} slug");
					}
				}
				// TODO: Move timespan to config
				_memoryCache.Set(cacheKey, post, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(120)));
				_logger.LogInformation($"{cacheKey} updated from source");
			}
			else
			{
				_logger.LogInformation($"{cacheKey} retrieved from cache");
			}

			return post;
		}

		public async Task DeleteAsync(IPost post)
		{
			await Task.Run(() =>
			{
				var cacheKey = string.Format(_cacheKeyPosts, post.BlogId);
				var postToDelete = new Post() { Id = post.Id };
				_context.Posts.Attach(postToDelete);
				_context.Posts.Remove(postToDelete);

				_memoryCache.Remove(cacheKey);
				_logger.LogInformation($"{cacheKey} cleared.");
			});
		}
	}
}