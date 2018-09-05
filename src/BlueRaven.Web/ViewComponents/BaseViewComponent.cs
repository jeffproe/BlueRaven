using BlueRaven.Data.Domain;
using BlueRaven.Svc;
using BlueRaven.Web.Framework;
using Microsoft.AspNetCore.Mvc;

namespace BlueRaven.Web.ViewComponents
{
	public abstract class BaseViewComponent : ViewComponent
	{
		protected BaseViewComponent(BlogService blogSvc, IApplicationContext appContext)
		{
			Blog = appContext.CurrentBlog;
			BlogSvc = blogSvc;
		}
		public IBlog Blog { get; }
		protected BlogService BlogSvc { get; set; }
	}
}
