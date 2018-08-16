using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BlueRaven.Web.ViewComponents
{
	public class DateIconViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(DateTime date)
		{
			return View(date);
		}
	}
}