using System.Threading.Tasks;
using BlueRaven.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlueRaven.Web.ViewComponents
{
	public class PagerViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(BlogModel model)
		{
			return View(model);
		}
	}
}