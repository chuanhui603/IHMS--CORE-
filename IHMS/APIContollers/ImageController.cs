using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Dapper;
using Microsoft.Extensions.Configuration; // 引入這個名稱空間以使用 IConfiguration

namespace IHMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration; // 添加一個 IConfiguration 屬性
        private string _connectionString; // 變更為實例變數

        // 透過 DI，注入 IConfiguration 到您的控制器中
        public ImageController(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            // 從 appsettings.json 中讀取名為 DefaultConnection 的連接字串，並將其賦值給 _connectionString 變量
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
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
