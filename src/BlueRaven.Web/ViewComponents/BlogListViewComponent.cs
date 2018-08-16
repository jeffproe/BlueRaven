using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlueRaven.Web.Models;

namespace BlueRaven.Web.ViewComponents
{
	public class BlogListViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(PagedPosts posts)
		{
			return View(posts);
		}
	}
}