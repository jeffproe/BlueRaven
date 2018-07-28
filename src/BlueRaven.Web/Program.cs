using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlueRaven.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BlueRaven.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateWebHostBuilder(args).Build();

			using (var scope = host.Services.CreateScope())
			{
				var services = scope.ServiceProvider;

				try
				{
					BRDbInitialization.InitializeDatabase(services);
				}
				catch (Exception ex)
				{
					// TODO: Logger
					//logger.Error(ex, "An error occurred Initializing the DB.");
				}
			}

			try
			{
				host.Run();
			}
			catch (Exception ex)
			{
				// TODO: Logger
				//logger.Error(ex, "Application stopped because of exception");
			}
		}

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
