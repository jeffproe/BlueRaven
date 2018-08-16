using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BlueRaven.Data
{
	public class BRContextFactory : IDesignTimeDbContextFactory<BRContext>
	{
		public BRContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<BRContext>();
			optionsBuilder.UseSqlite("Data Source=../BlueRaven.Web/app.db");

			return new BRContext(optionsBuilder.Options);
		}
	}
}