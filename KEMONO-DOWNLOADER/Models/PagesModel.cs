namespace KEMONO_DOWNLOADER.Models
{
    public class PagesModel
    {
        public List<PagesPostsModel> Posts { get; set; }
        public string Url { get; set; }
        public string Author { get; set; }
        public string UserId { get; set; }
        public string Service { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public bool FilterFuta { get; set; }
        public DownloaderType Site { get; set; }
    }

    public class PagesPostsModel
    {
        public string PostId { get; set; }
        public List<PostWithLink> Posts { get; set; }
        public string PostName { get; set; }
    }
}
