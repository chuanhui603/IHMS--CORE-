using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;

namespace IHMS.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageBoardController : ControllerBase
    {


        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly string _connectionString;

        public MessageBoardController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            // 在建構子中取得資料庫連接字串
            _webHostEnvironment = webHostEnvironment;
            _connectionString = configuration.GetConnectionString("IHMSConnection");
        }

        // GET: api/MessageBoard
        [HttpGet]
        public IActionResult GetMessages()
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    dbConnection.Open();
                    var query = @"SELECT m.message_id, m.title, m.contents, m.category, m.member_id, m.time, mem.name 
                FROM [dbo].[message board] m
                INNER JOIN [dbo].[Members] mem ON m.member_id = mem.member_id
                ORDER BY m.time DESC";
                    var messages = dbConnection.Query<Message>(query).AsList();
                    return Ok(messages);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "資料庫查詢失敗。");
            }
        }

        // GET: api/MessageBoard/5
        [HttpGet("{id}")]
        public IActionResult GetMessage(int id)
        {
            try
            {
                // 使用 Dapper 執行 SQL 查詢，取得指定留言板資料
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    dbConnection.Open();
                    var query = @"SELECT m.message_id, m.title, m.contents, m.category, m.member_id, m.time, mem.name 
                FROM [dbo].[message board] m
                INNER JOIN [dbo].[Members] mem ON m.member_id = mem.member_id
                WHERE m.message_id = @MessageId";
                    var message = dbConnection.QuerySingleOrDefault<Message>(query, new { MessageId = id });

                    if (message == null)
                    {
                        return NotFound();
                    }

                    return Ok(message);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "資料庫查詢失敗。");
            }
        }

        // POST: api/MessageBoard
        [HttpPost("CreateMessage")]
        public IActionResult CreateMessage([FromForm] Message message, [FromForm] List<IFormFile> images)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    dbConnection.Open();

                    // log 或 console
                    Debug.WriteLine($"Received message: Title={message.Title}, Contents={message.Contents}, Category={message.Category}, MemberId={message.member_id}");

                    message.Time = DateTime.Now;

                    var query = "INSERT INTO [dbo].[message board] (title, contents, category, member_id, time) " +
                        "VALUES (@Title, @Contents, @Category, @Member_Id, @Time); SELECT CAST(SCOPE_IDENTITY() as int);";
                    int messageId = dbConnection.ExecuteScalar<int>(query, message);

                    var webRootPath = _webHostEnvironment.WebRootPath;
                    var imageDirectory = Path.Combine(webRootPath, "messageimage");

                    if (!Directory.Exists(imageDirectory))
                    {
                        Directory.CreateDirectory(imageDirectory);
                    }

                    if (images != null && images.Count > 0)
                    {
                        foreach (var image in images)
                        {
                            string fileName = $"{messageId}_{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                            string filePath = Path.Combine(imageDirectory, fileName);

                            //  log 或 console
                            Debug.WriteLine($"Received image: Name={image.FileName}, Size={image.Length}");

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                image.CopyTo(stream);
                            }

                            var imageQuery = "INSERT INTO [dbo].[message board image] (message_id, image) VALUES (@MessageId, @Image)";
                            dbConnection.Execute(imageQuery, new { MessageId = messageId, Image = fileName });
                        }
                    }

                    return CreatedAtAction(nameof(GetMessage), new { id = messageId }, message);
                }
            }
            catch (Exception ex)
            {
                // 在這裡加入 log 或 console 輸出，觀察錯誤訊息
                Debug.WriteLine($"Error occurred: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, "新增留言板資料失敗。");
            }
        }
        // PUT: api/MessageBoard/5
        [HttpPut("{id}")]
        public IActionResult UpdateMessage(int id, [FromBody] Message message)
        {
            try
            {
                // 使用 Dapper 執行 SQL 更新，修改指定留言板資料
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    dbConnection.Open();
                    var query = "UPDATE [dbo].[message board] SET title = @Title, contents = @Contents, " +
                        "category = @Category, member_id = @MemberId, time = @Time WHERE message_id = @MessageId";
                    message.message_id = id; // 確保傳入的 Message 物件的 MessageId 正確
                    dbConnection.Execute(query, message);
                    return Ok(message);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "更新留言板資料失敗。");
            }
        }

        // DELETE: api/MessageBoard/5
        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(int id)
        {
            try
            {
                // 使用 Dapper 執行 SQL 刪除，刪除指定留言板資料
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    dbConnection.Open();
                    var query = "DELETE FROM [dbo].[message board] WHERE message_id = @MessageId";
                    dbConnection.Execute(query, new { MessageId = id });
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "刪除留言板資料失敗。");
            }
        }
    }
    public class Message
    {
        public int message_id { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public string Category { get; set; }
        public int member_id { get; set; }
        public DateTime Time { get; set; }
        public string Name { get; set; }
       
    }
}
