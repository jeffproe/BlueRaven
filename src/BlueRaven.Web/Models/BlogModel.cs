using System.Collections.Generic;
using BlueRaven.Data.Domain;

namespace BlueRaven.Web.Models
{
	public class BlogModel
	{
		public IBlog Blog { get; set; }
		public IEnumerable<IPost> Posts { get; set; }
		public IPost Post { get; set; }

		private int _page;
		public int Page
		{
			get
			{
				return _page;
			}
			set
			{
				_page = value;
				UpdatePage();
			}
		}

		private int _pageCount;
		public int PageCount
		{
			get
			{
				return _pageCount;
			}
			set
			{
				_pageCount = value;
				UpdatePage();
			}
		}
		public bool HasPreviousPage
		{
			get
			{
				return Page != 1;
			}
		}

		public bool HasNextPage
		{
			get
			{
				return Page != PageCount;
			}
		}

		public int NextPage
		{
			get
			{
				return Page + 1;
			}
		}

		public int PreviousPage
		{
			get
			{
				return Page - 1;
			}
		}

		private void UpdatePage()
		{
			if (_page > _pageCount)
			{
				_page = _pageCount;
			}
			if (_page < 1)
			{
				_page = 1;
			}
		}
	}
}