using System.Threading.Tasks;
using BlueRaven.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlueRaven.Web.ViewComponents
{
	public class PostViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(Post post)
		{
			return View(post);
		}
	}
}