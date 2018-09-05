using BlueRaven.Data.Domain;
using BlueRaven.Data.Models;
using BlueRaven.Svc;
using BlueRaven.Web.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlueRaven.Web.Controllers
{
	public class BaseController : Controller
	{

		protected BlogService _blogService;
		protected PostService _postService;
		protected ILogger _logger;
		protected IApplicationContext _appContext;

		public BaseController(BlogService blogService, PostService postService, ILogger<HomeController> logger, IApplicationContext appContext)
		{
			_blogService = blogService;
			_postService = postService;
			_logger = logger;
			_appContext = appContext;

			Blog = appContext.CurrentBlog;
		}

		public IBlog Blog { get; }
	}
}