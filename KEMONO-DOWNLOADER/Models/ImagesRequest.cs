namespace KEMONO_DOWNLOADER.Models
{
    public class ImagesRequest
    {
        public int Page { get; set; }
        public string Url { get; set; }
        public bool FilterFuta { get; set; }
        public DownloaderType Site { get; set; }
    }

    public enum DownloaderType
    {
        Kemono = 1,
        Coomer
    }
}
