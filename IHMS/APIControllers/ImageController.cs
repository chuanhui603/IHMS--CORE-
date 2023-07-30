using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace IHMS.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private string _connectionString; // 變更為實例變數
        // DI，注入 IConfiguration 到控制器
        public ImageController(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("IHMSConnection");
        }

        [HttpGet("{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "AnnouncementImage", imageName);
            byte[] imageData = System.IO.File.ReadAllBytes(imagePath);
            return File(imageData, "image/jpeg");
        }

        [HttpGet("{messageId}/images")]
        public IActionResult GetMessageImages(int messageId)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                var query = "SELECT [image] FROM [dbo].[message board image] WHERE message_id = @MessageId";
                var images = dbConnection.Query<string>(query, new { MessageId = messageId }).ToList();

                return Ok(images);
            }
        }
        [HttpGet("{messageId}/images/{imageName}")]
        public IActionResult GetMessageImage(int messageId, string imageName)
        {
            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "messageimage", imageName);
            if (System.IO.File.Exists(imagePath))
            {
                byte[] imageData = System.IO.File.ReadAllBytes(imagePath);
                if (imageData.Length == 0)
                {
                    Console.WriteLine("圖片數據為空。");
                    return NotFound();
                }
                return File(imageData, "image/jpeg");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
