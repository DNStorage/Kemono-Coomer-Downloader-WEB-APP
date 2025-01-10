namespace KEMONO_DOWNLOADER.Models
{
    public class DownloadImagesModel
    {
        public List<string> SelectedContent {  get; set; }
        public string AuthorName { get; set; }
        public int Page { get; set; }
    }
}
