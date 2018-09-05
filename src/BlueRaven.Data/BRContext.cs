using System;
using System.Collections.Generic;
using System.Text;
using BlueRaven.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlueRaven.Data
{
	public class BRContext : IdentityDbContext<IdentityUser>
	{
		public BRContext(DbContextOptions<BRContext> options)
			: base(options)
		{
		}

		public DbSet<Blog> Blogs { get; set; }
		public DbSet<Post> Posts { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			// Customize the ASP.NET Identity model and override the defaults if needed.
			// For example, you can rename the ASP.NET Identity table names and more.
			// Add your customizations after calling base.OnModelCreating(builder);
			builder.Entity<Blog>().HasData(new Blog
			{
				Id = "blog1",
				Title = "Blue Raven",
				ByLine = "Powered by Blue Raven",
				Disclaimer = "Read at your own risk!",
				LocalUrl = "https://localhost:5001",
				Url = "https://example.com",
				Theme = "Default"
			});

			builder.Entity<Post>().HasData(
				new Post
				{
					Id = 1,
					BlogId = "blog1",
					Title = "First post",
					Content = "Bacon ipsum dolor amet doner drumstick swine prosciutto. Swine boudin chuck tongue flank beef venison. Turducken strip steak beef ribs boudin. Pastrami jowl pork loin tri-tip. Ham hock biltong buffalo, pork short ribs spare ribs turducken strip steak prosciutto swine porchetta venison bresaola tongue doner. Porchetta bresaola pork chop venison. Boudin picanha burgdoggen drumstick meatball pork chop cow cupim shoulder tenderloin turducken filet mignon.\nFatback sausage chicken spare ribs, rump pork chop picanha strip steak buffalo. Burgdoggen beef frankfurter, spare ribs hamburger pork loin alcatra pork belly doner ground round biltong. Strip steak short loin leberkas ball tip salami brisket drumstick spare ribs. Ham bacon buffalo bresaola, short loin cupim flank rump pastrami tenderloin porchetta venison ball tip burgdoggen t-bone. Swine hamburger pork prosciutto pig pork belly jerky venison flank shoulder bacon pancetta. Salami pork chop rump, biltong kielbasa filet mignon pig.\nBall tip burgdoggen bresaola pancetta. Biltong pork loin buffalo, hamburger flank beef ribs shankle pig ground round ball tip. Pork loin kevin beef ribs turducken, flank ham hock short loin cupim beef t-bone ribeye ground round pig fatback. Kevin salami beef swine. Flank pig burgdoggen sirloin.",
					Author = "Me",
					IsPublished = true,
					PubDate = new DateTime(2018, 7, 6)
				},
				new Post
				{
					Id = 2,
					BlogId = "blog1",
					Title = "Next post",
					Content = "Spicy jalapeno ground round short loin pork chop, beef ribs t-bone meatball beef. Beef ribs tail filet mignon, drumstick sirloin kevin fatback jerky flank swine buffalo prosciutto. Short loin chuck capicola pastrami turkey bacon filet mignon. Pork chop pancetta beef boudin. Ball tip swine ground round tongue short ribs tenderloin pig landjaeger flank fatback shankle drumstick cupim.\nBresaola t-bone tri-tip chicken ham hock shankle brisket. Landjaeger shank doner meatball, tenderloin rump prosciutto frankfurter jerky. Filet mignon salami beef fatback beef ribs, andouille strip steak meatloaf prosciutto t-bone pork belly brisket ground round tail. Tenderloin jowl ribeye swine venison, short loin spare ribs short ribs burgdoggen pork chop shank shoulder. Hamburger bacon burgdoggen, turducken boudin ground round filet mignon flank shoulder. Pig meatloaf ham, hamburger turkey porchetta pastrami rump. Salami chicken porchetta, shoulder ham landjaeger t-bone turkey pancetta.\nShoulder boudin pork brisket, picanha meatball tongue sausage ball tip spare ribs venison flank leberkas burgdoggen pork belly. Flank spare ribs rump leberkas, beef ribs andouille ham hock tri-tip alcatra doner t-bone. Tongue cupim jowl, jerky chuck prosciutto chicken ball tip shank fatback buffalo doner flank. Beef ribs sausage t-bone landjaeger flank salami pork kevin beef bacon short ribs pork loin ham ball tip. Sausage beef buffalo, cupim kielbasa kevin turkey. Pork loin bresaola tri-tip pork chop jerky salami corned beef ball tip tail. Ham cow pig turkey, t-bone beef leberkas boudin shoulder burgdoggen prosciutto meatloaf pastrami tail.",
					Author = "Me",
					IsPublished = true,
					PubDate = new DateTime(2018, 7, 13)
				}
			);
		}
	}
}
