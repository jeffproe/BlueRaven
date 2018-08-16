using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlueRaven.Web.Models;
using BlueRaven.Svc;
using Microsoft.Extensions.Logging;
using BlueRaven.Data.Domain;

namespace BlueRaven.Web.Controllers
{
	[Route("")]
	public class HomeController : Controller
	{
		public HomeController(BlogService blogService, PostService postService, ILogger<HomeController> logger)
		{
			_blogService = blogService;
			_postService = postService;
			_logger = logger;
		}

		private BlogService _blogService;
		private PostService _postService;
		private ILogger _logger;

		[HttpGet("")]
		public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
		{
			var pageData = new PagedPosts();
			// TODO: get the blog Id for real
			pageData.Blog = _blogService.GetById("blog1");
			var ret = await _postService.GetPagedAsync(pageData.Blog.Id, page, pageSize);
			pageData.Posts = ret.list;
			pageData.PageCount = ret.pageCount;
			pageData.Page = page;
			return View(pageData);
		}

		[HttpGet("{year:int}/{month:int}/{day:int}/{slug}")]
		public async Task<IActionResult> SinglePost(int year, int month, int day, string slug)
		{
			try
			{
				// TODO: get the blog Id for real
				IPost post = await _postService.GetBySlugAsync("blog1", year, month, day, slug);

				if (post != null)
				{
					return View(post);
				}
			}
			catch
			{
				_logger.LogWarning($"Couldn't find the {year}/{month}/{day}/{slug} post");
			}

			return Redirect("/");
		}

		[HttpGet("about")]
		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		[HttpGet("contact")]
		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		[HttpGet("privacy")]
		public IActionResult Privacy()
		{
			return View();
		}

		[HttpGet("error")]
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
