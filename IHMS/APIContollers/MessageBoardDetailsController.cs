using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace IHMS.APIContollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageBoardDetailsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MessageBoardDetailsController(IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            _config = config;
            _webHostEnvironment = webHostEnvironment;
        }

        // 用於查詢的類別
        public class MessageBoardDetailQuery
        {
            public int message_id { get; set; }
            public string Contents { get; set; }
            public int member_id { get; set; }
            public DateTime time { get; set; }
            public string image { get; set; } 
        }

        // 用於上傳的類別
        public class MessageBoardDetailUpload
        {
            public int message_id { get; set; }
            public string Contents { get; set; }
            public int member_id { get; set; }
            public IFormFile image { get; set; } 
        }

        [HttpGet("{messageId}")]
        public async Task<IActionResult> Get(int messageId)
        {
            var sql = "SELECT * FROM [dbo].[message board details] WHERE message_id = @messageId";

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var results = await connection.QueryAsync<MessageBoardDetailQuery>(sql, new { messageId });
                return Ok(results);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] MessageBoardDetailUpload newComment)
        {
            var filePath = "";
            if (newComment.image != null)
            {
                // Create a unique filename
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + newComment.image.FileName;
                // Specify the file path
                filePath = Path.Combine(_webHostEnvironment.WebRootPath, "messagedetailimage", uniqueFileName);

                // Save the file
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await newComment.image.CopyToAsync(fileStream);
                }
            }
            else
            {
                filePath = "default_image.jpg"; // or whatever default value you want
            }

            var sql = @"INSERT INTO [dbo].[message board details] (message_id, Contents, member_id, time, image) 
VALUES (@message_id, @Contents, @member_id, GETDATE(), @image)";

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var parameters = new
                {
                    message_id = newComment.message_id,
                    Contents = newComment.Contents,
                    member_id = newComment.member_id,
                    image = filePath
                };
                var affectedRows = await connection.ExecuteAsync(sql, parameters);
                return Ok(new { message = $"{affectedRows} rows inserted" });
            }
        }
    }
}
