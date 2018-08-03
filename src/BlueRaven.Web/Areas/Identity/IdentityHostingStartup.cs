using System;
using BlueRaven.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(BlueRaven.Web.Areas.Identity.IdentityHostingStartup))]
namespace BlueRaven.Web.Areas.Identity
{
	public class IdentityHostingStartup : IHostingStartup
	{
		public void Configure(IWebHostBuilder builder)
		{
			builder.ConfigureServices((context, services) =>
			{
				services.AddDbContext<BRContext>(options =>
					//options.UseSqlServer(
					options.UseSqlite(
						context.Configuration.GetConnectionString("DefaultConnection")));

				services.AddDefaultIdentity<IdentityUser>()
					.AddEntityFrameworkStores<BRContext>();
			});
		}
	}
}