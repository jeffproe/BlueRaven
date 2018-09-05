using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlueRaven.Data.Domain;
using BlueRaven.Data.Models;
using BlueRaven.Svc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BlueRaven.Web.Framework
{
	/// <summary>
	/// The ApplicationContextMiddleware component processes the request to built the ApplicationContext object used
	/// for the duration of the request.  
	/// The IApplicationContext object is passed in by Dependency Injection with a "scoped" lifetime, ensuring that a new object
	/// is created for each request.
	/// </summary>
	public class ApplicationContextMiddleware
	{
		RequestDelegate _next;
		private ILogger _logger;
		private IMemoryCache _memoryCache;
		private string _blogsCacheKey = "blogs";

		public ApplicationContextMiddleware(RequestDelegate next,
									IMemoryCache memoryCache,
									ILoggerFactory loggerFactory)
		{
			_memoryCache = memoryCache;
			_logger = loggerFactory.CreateLogger<ApplicationContextMiddleware>();
			_next = next;
		}

		public async Task Invoke(HttpContext context, IApplicationContext appContext, BlogService blogSvc)
		{
			appContext.Blogs = GetAllBlogs(blogSvc);
			if (context.Request.IsLocal())
			{
				appContext.CurrentBlog = appContext.Blogs.Single(b => b.LocalUrl == context.Request.Host.Value);
			}
			else
			{
				appContext.CurrentBlog = appContext.Blogs.Single(b => b.Url == context.Request.Host.Value);
			}
			await _next.Invoke(context);
		}

		private IEnumerable<IBlog> GetAllBlogs(BlogService blogSvc)
		{
			IEnumerable<IBlog> blogs;

			if (!_memoryCache.TryGetValue(_blogsCacheKey, out blogs))
			{
				// fetch the value from the source
				blogs = blogSvc.GetAll().ToList();

				// store in the cache
				_memoryCache.Set(_blogsCacheKey,
					blogs,
					new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(2)));
				_logger.LogInformation($"{_blogsCacheKey} updated from source.");
			}
			else
			{
				_logger.LogInformation($"{_blogsCacheKey} retrieved from cache.");
			}

			return blogs;
		}
	}
}