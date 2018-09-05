using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlueRaven.Data
{
	public static class BRDbInitialization
	{
		public static void InitializeDatabase(IServiceProvider services)
		{
			PerformMigrations(services);
			SeedData(services);
		}

		private static void PerformMigrations(IServiceProvider services)
		{
			try
			{
				services.GetRequiredService<BRContext>().Database.Migrate();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}

		private static void SeedData(IServiceProvider services)
		{
			// TODO: Add any seed data we might need...
		}
	}
}