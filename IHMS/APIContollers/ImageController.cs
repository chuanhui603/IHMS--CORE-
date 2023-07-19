using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace IHMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "AnnouncementImage", imageName);
            byte[] imageData = System.IO.File.ReadAllBytes(imagePath);
            return File(imageData, "image/jpeg");
        }


        [HttpPost("upload")]
        public IActionResult UploadImage()
        {
            try
            {
                var files = Request.Form.Files;
                if (files == null || files.Count == 0)
                {
                    return BadRequest("沒有上傳任何圖片。");
                }

                var imageDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "AnnouncementImage");

                if (!Directory.Exists(imageDirectory))
                {
                    Directory.CreateDirectory(imageDirectory);
                }

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        // 檔案名稱使用 GUID 組合，避免檔案名稱重複
                        string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                        string filePath = Path.Combine(imageDirectory, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }
                }

                return Ok("圖片上傳成功。");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "圖片上傳失敗。");
            }
        }
    }
}
