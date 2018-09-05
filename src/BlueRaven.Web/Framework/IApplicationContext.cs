using System;
using System.Collections.Generic;
using BlueRaven.Data.Domain;
using BlueRaven.Data.Models;

namespace BlueRaven.Web.Framework
{
	public interface IApplicationContext
	{
		Guid Id { get; }
		IEnumerable<IBlog> Blogs { get; set; }
		IBlog CurrentBlog { get; set; }
	}
}