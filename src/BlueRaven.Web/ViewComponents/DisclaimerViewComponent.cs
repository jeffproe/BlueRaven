using System.Threading.Tasks;
using BlueRaven.Svc;
using BlueRaven.Web.Framework;
using Microsoft.AspNetCore.Mvc;

namespace BlueRaven.Web.ViewComponents
{
	public class DisclaimerViewComponent : BaseViewComponent
	{
		public DisclaimerViewComponent(BlogService blogSvc, IApplicationContext appContext)
		: base(blogSvc, appContext)
		{

		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View(Blog);
		}
	}
}