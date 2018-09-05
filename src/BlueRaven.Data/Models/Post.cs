using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text;
using BlueRaven.Data.Domain;
using BlueRaven.Data.Extensions;

namespace BlueRaven.Data.Models
{
	public class Post : IPost
	{
		private string _slug;

		public Post()
		{
			PubDate = DateTime.UtcNow;
			LastModified = DateTime.UtcNow;
			IsPublished = true;
			Keywords = String.Empty;
			Title = String.Empty;
			Author = String.Empty;
			Excerpt = String.Empty;
			Content = String.Empty;
			Categories = String.Empty;
		}

		// TODO: implement this as part of the MetaWeblogAPI task
		//[XmlRpcProperty("blogId")]
		public string BlogId { get; set; }
		//[XmlRpcProperty("postid")]
		public int Id { get; set; }
		//[XmlRpcProperty("title")]
		public string Title { get; set; }
		//[XmlRpcProperty("author")]
		public string Author { get; set; }
		//[XmlRpcProperty("wp_slug")]
		public string Slug
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_slug))
				{
					_slug = CreateSlug();
				}
				return _slug;
			}
			set
			{
				_slug = value;
			}
		}
		private string _excerpt;
		//[XmlRpcProperty("mt_excerpt")]
		public string Excerpt
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(_excerpt))
				{
					return _excerpt;
				}
				else
				{
					_excerpt = Content.TruncateHtml(250, "...");
					return _excerpt;
				}
			}
			set
			{
				_excerpt = value;
			}
		}
		//[XmlRpcProperty("description")]
		public string Content { get; set; }
		//[XmlRpcProperty("dateCreated")]
		public DateTime PubDate { get; set; }
		//[XmlRpcProperty("dateModified")]
		public DateTime LastModified { get; set; }
		//[XmlRpcProperty("categories")]
		public string Categories { get; set; }
		//[XmlRpcProperty("mt_keywords")]
		public string Keywords { get; set; }
		public bool IsPublished { get; set; }
		public List<string> Tags
		{
			get
			{
				List<string> tags = new List<string>();
				foreach (var tag in Keywords.Split(','))
				{
					tags.Add(tag.Trim());
				}
				return tags;
			}
		}
		[ForeignKey("BlogId")]
		public Blog Blog { get; set; }

		private string CreateSlug()
		{
			string title = Title.ToLowerInvariant().Replace(" ", "-");
			title = title.RemoveDiacritics();
			title = title.RemoveReservedUrlCharacters();

			return $"{PubDate.Year}/{PubDate.Month}/{PubDate.Day}/{title.ToLowerInvariant()}";
		}
	}
}