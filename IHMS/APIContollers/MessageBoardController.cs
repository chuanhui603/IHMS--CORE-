using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace IHMS.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageBoardController : ControllerBase
    {
        private readonly string _connectionString;

        public MessageBoardController(IConfiguration configuration)
        {
            // 在建構子中取得資料庫連接字串
            _connectionString = configuration.GetConnectionString("DefaultConnection");
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
                    var query = "SELECT * FROM [dbo].[message board] ORDER BY time DESC";
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
                    var query = "SELECT * FROM [dbo].[message board] WHERE message_id = @MessageId";
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
        [HttpPost]
        public IActionResult CreateMessage([FromBody] Message message)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                {
                    dbConnection.Open();
                    
                    message.Time = DateTime.Now; 

                    var query = "INSERT INTO [dbo].[message board] (title, contents, category, member_id, time) " +
                        "VALUES (@Title, @Contents, @Category, @Member_Id, @Time)";
                    dbConnection.Execute(query, message);
                    return CreatedAtAction(nameof(GetMessage), new { id = message.message_id }, message);
                }
            }
            catch (Exception ex)
            {
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
    }
}
