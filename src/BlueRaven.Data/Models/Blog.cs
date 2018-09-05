using System.Collections.Generic;
using BlueRaven.Data.Domain;

namespace BlueRaven.Data.Models
{
	public class Blog : IBlog
	{
		public string Id { get; set; }
		public string ByLine { get; set; }
		public string Disclaimer { get; set; }
		public string LocalUrl { get; set; }
		public string Theme { get; set; }
		public string Title { get; set; }
		public string Url { get; set; }
		public List<Post> Posts { get; set; }
		public string FaviconUrl { get; set; }
	}
}