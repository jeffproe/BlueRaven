using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlueRaven.Data
{
	public class BRContext : IdentityDbContext
	{
		public BRContext(DbContextOptions<BRContext> options)
			: base(options)
		{
		}
	}
}
