namespace BlueRaven.Data.Domain
{
	public interface IBlog
	{
		string Id { get; set; }
		string ByLine { get; set; }
		string Disclaimer { get; set; }
		string LocalUrl { get; set; }
		string Theme { get; set; }
		string Title { get; set; }
		string Url { get; set; }
		string FaviconUrl { get; set; }
	}
}