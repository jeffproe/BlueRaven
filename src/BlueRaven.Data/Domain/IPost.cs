using System;
using System.Collections.Generic;

namespace BlueRaven.Data.Domain
{
	public interface IPost
	{
		string BlogId { get; set; }
		int Id { get; set; }
		string Title { get; set; }
		string Author { get; set; }
		string Slug { get; set; }
		string Excerpt { get; set; }
		string Content { get; set; }
		DateTime PubDate { get; set; }
		DateTime LastModified { get; set; }
		string Categories { get; set; }
		string Keywords { get; set; }
		bool IsPublished { get; set; }
		List<string> Tags { get; }
	}
}