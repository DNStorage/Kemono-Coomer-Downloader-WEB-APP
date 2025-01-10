using System.Diagnostics;
using System.IO.Compression;
using System.Resources;
using System.Text.Json;
using KEMONO_DOWNLOADER.Models;
using Microsoft.AspNetCore.Mvc;

namespace KEMONO_DOWNLOADER.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        #region Extentions
        // Common raster image file suffixes
        string[] imageSuffixes = new string[]
        {
            ".jpg",  // JPEG image
            ".jpeg", // JPEG image
            ".png",  // Portable Network Graphics
            ".gif",  // Graphics Interchange Format
            ".bmp",  // Bitmap image
            ".tiff", // Tagged Image File Format
            ".tif",  // Tagged Image File Format (alternative)
            ".webp", // WebP image
            ".svg",  // Scalable Vector Graphics (not raster, but often treated as such)
            ".ico",  // Icon format
            ".heif", // High Efficiency Image File Format
            ".heic"  // High Efficiency Image Coding
        };

        // Common video file suffixes
        string[] videoSuffixes = new string[]
        {
            ".mp4",  // MPEG-4 video
            ".mov",  // QuickTime movie
            ".avi",  // Audio Video Interleave
            ".mkv",  // Matroska Video
            ".flv",  // Flash Video
            ".wmv",  // Windows Media Video
            ".webm", // WebM video
            ".m4v",  // MPEG-4 Video file
            ".3gp",  // 3GPP Multimedia
            ".3g2",  // 3GPP2 Multimedia
            ".ts",   // MPEG Transport Stream
            ".mpeg", // MPEG Video
            ".mpg",  // MPEG Video (alternative)
            ".vob",  // Video Object
            ".rm",   // RealMedia
            ".ogv"   // Ogg Video
        };
        #endregion

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Images([FromForm] ImagesRequest request)
        {
            string urlToUse = request.Url;

            if (urlToUse.Contains("kemono.su"))
            {
                urlToUse = urlToUse.Substring(urlToUse.IndexOf("kemono.su") + "kemono.su".Count() + 1);
            }
            else if (urlToUse[0] == '/')
            {
                request.Url.Substring(1);
            }

            var pageNumber = request.Page == 0 ? "" : $"?o={request.Page * 50}";
            var url = $"https://kemono.su/api/v1/{urlToUse}/posts-legacy{pageNumber}";

            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            var responseString = await response.Content.ReadAsStringAsync();
            var posts = JsonSerializer.Deserialize<PostsModel>(responseString);

            List<PostWithLink> links = new List<PostWithLink>();
            int server = 1;

            foreach (var post in posts.results)
            {
                if (post.title.Contains("futa", StringComparison.OrdinalIgnoreCase) && request.FilterFuta) continue;
                foreach(var attachment in post.attachments)
                {
                    if (attachment.name.Contains("futa", StringComparison.OrdinalIgnoreCase) && request.FilterFuta) continue;

                    if (imageSuffixes.Any(attachment.name.Contains))
                        links.Add(new PostWithLink { Thumbnail = $"https://img.kemono.su/thumbnail/data{attachment.path}", Link = $"https://n{server}.kemono.su/data{attachment.path}", Name = attachment.name, IsImage = true });
                    else if(videoSuffixes.Any(attachment.name.Contains))
                        links.Add(new PostWithLink { Link = $"https://n{server}.kemono.su/data{attachment.path}", Name = attachment.name, IsVideo = true });
                    else
                        links.Add(new PostWithLink { Link = $"https://n{server}.kemono.su/data{attachment.path}", Name = attachment.name });

                    if (++server > 4)
                        server = 1;
                }
            }
            var x = new PagesModel { Posts = links, CurrentPage = request.Page, Pages = (int)Math.Ceiling(posts.props.count / 50d), Url = request.Url, FilterFuta = request.FilterFuta };
            return View(x);
        }

        [HttpPost]
        public async Task<IActionResult> DownloadImages([FromForm] List<string> selectedContent)
        {
            if (selectedContent == null || !selectedContent.Any())
            {
                return BadRequest("No images selected.");
            }

            var tempZipPath = Path.GetTempFileName(); // Temporary ZIP file path

            using (var zip = ZipFile.Open(tempZipPath, ZipArchiveMode.Update))
            {
                foreach (var imageUrl in selectedContent)
                {
                    // Download the image data
                    using var client = new HttpClient();
                    var imageData = await client.GetByteArrayAsync(imageUrl);
                    var fileName = Path.GetFileName(new Uri(imageUrl).LocalPath);

                    // Add the image to the ZIP file
                    var entry = zip.CreateEntry(fileName);
                    using var entryStream = entry.Open();
                    await entryStream.WriteAsync(imageData, 0, imageData.Length);
                }
            }

            // Return the ZIP file as a downloadable response
            var zipFileName = "SelectedImages.zip";
            return File(System.IO.File.ReadAllBytes(tempZipPath), "application/zip", zipFileName);
        }
    }
}
