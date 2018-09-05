using System;
using System.Collections.Generic;
using BlueRaven.Data.Domain;
using BlueRaven.Data.Models;

namespace BlueRaven.Web.Framework
{
	public class ApplicationContext : IApplicationContext
	{
		public ApplicationContext()
		{
			Id = Guid.NewGuid();
		}

		public Guid Id { get; }
		public IEnumerable<IBlog> Blogs { get; set; }
		public IBlog CurrentBlog { get; set; }
	}
}