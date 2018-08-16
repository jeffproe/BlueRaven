using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace BlueRaven.Svc
{
	public class BaseService
	{
		protected readonly IMemoryCache _memoryCache;
		protected ILogger _logger;
		protected readonly string _cacheKeyPosts;
		protected readonly string _cacheKeyPost;

		public BaseService(IHostingEnvironment env, IMemoryCache memoryCache, ILogger logger)
		{
			_memoryCache = memoryCache;
			_logger = logger;
			_cacheKeyPosts = "{0}_posts";
			_cacheKeyPost = "{0}:{1}";
		}
	}
}