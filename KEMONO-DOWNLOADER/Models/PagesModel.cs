namespace KEMONO_DOWNLOADER.Models
{
    public class PagesModel
    {
        public List<PostWithLink> Posts { get; set; }
        public string Url { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public bool FilterFuta { get; set; }
    }
}
