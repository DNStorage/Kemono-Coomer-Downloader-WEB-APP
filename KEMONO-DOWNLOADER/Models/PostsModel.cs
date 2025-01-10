namespace KEMONO_DOWNLOADER.Models
{
    public class PostsModel
    {
        public Props props { get; set; }
        public Base _base { get; set; }
        public Result[] results { get; set; }
        public Result_Previews[][] result_previews { get; set; }
        public object[][] result_attachments { get; set; }
        public bool[] result_is_image { get; set; }
        public bool disable_service_icons { get; set; }
    }

    public class Props
    {
        public string currentPage { get; set; }
        public string id { get; set; }
        public string service { get; set; }
        public string name { get; set; }
        public int count { get; set; }
        public int limit { get; set; }
        public Artist artist { get; set; }
        public Display_Data display_data { get; set; }
        public int dm_count { get; set; }
        public int share_count { get; set; }
        public string has_links { get; set; }
    }

    public class Artist
    {
        public string id { get; set; }
        public string name { get; set; }
        public string service { get; set; }
        public DateTime indexed { get; set; }
        public DateTime updated { get; set; }
        public string public_id { get; set; }
        public int? relation_id { get; set; }
    }

    public class Display_Data
    {
        public string service { get; set; }
        public string href { get; set; }
    }

    public class Base
    {
        public string service { get; set; }
        public string artist_id { get; set; }
    }

    public class Result
    {
        public string id { get; set; }
        public string user { get; set; }
        public string service { get; set; }
        public string title { get; set; }
        public string substring { get; set; }
        public DateTime published { get; set; }
        public File file { get; set; }
        public Attachment[] attachments { get; set; }
    }

    public class File
    {
        public string name { get; set; }
        public string path { get; set; }
    }

    public class Attachment
    {
        public string name { get; set; }
        public string path { get; set; }
    }

    public class Result_Previews
    {
        public string type { get; set; }
        public string server { get; set; }
        public string name { get; set; }
        public string path { get; set; }
    }

    public class Result_Attachments
    {
        public string path { get; set; }
        public string name { get; set; }
        public string server { get; set; }
    }

}
