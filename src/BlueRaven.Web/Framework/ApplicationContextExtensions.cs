using Microsoft.AspNetCore.Builder;

namespace BlueRaven.Web.Framework
{
	public static class ApplicationContextExtensions
	{
		public static IApplicationBuilder UseApplicationContext(this IApplicationBuilder app)
		{
			return app.UseMiddleware<ApplicationContextMiddleware>();
		}
	}
}