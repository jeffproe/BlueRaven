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
using BlueRaven.Web.Framework;

namespace BlueRaven.Web.Controllers
{
	[Route("")]
	public class HomeController : BaseController
	{
		public HomeController(BlogService blogService, PostService postService, ILogger<HomeController> logger, IApplicationContext appContext)
		: base(blogService, postService, logger, appContext)
		{

		}


		[HttpGet("")]
		public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
		{
			var pageData = new BlogModel();
			pageData.Blog = Blog;
			var ret = await _postService.GetPagedAsync(Blog.Id, page, pageSize);
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
				var singlePost = new BlogModel()
				{
					Blog = Blog,
					Post = await _postService.GetBySlugAsync(Blog.Id, year, month, day, slug)
				};

				if (singlePost.Post != null)
				{
					return View(singlePost);
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

			var model = new BlogModel()
			{
				Blog = Blog
			};

			return View(model);
		}

		[HttpGet("contact")]
		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			var model = new BlogModel()
			{
				Blog = Blog
			};

			return View(model);
		}

		[HttpGet("privacy")]
		public IActionResult Privacy()
		{
			var model = new BlogModel()
			{
				Blog = Blog
			};
			return View(model);
		}

		[HttpGet("error")]
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
