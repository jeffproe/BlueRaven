using System.Linq;
using BlueRaven.Data;
using BlueRaven.Data.Domain;

namespace BlueRaven.Svc
{
	public class BlogService
	{
		public BlogService(BRContext context)
		{
			_context = context;
		}

		private BRContext _context;

		public IBlog GetById(string blogId)
		{
			var blog = _context.Blogs.FirstOrDefault(b => b.Id == blogId);
			return blog;
		}
	}
}