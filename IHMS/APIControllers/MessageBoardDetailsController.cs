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

namespace IHMS.APIControllers
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
            //public string image { get; set; }
        }




        [HttpGet("{messageId}")]
        public async Task<IActionResult> Get(int messageId)
        {
            var sql = "SELECT message_id, Contents, member_id, time FROM [dbo].[message board details] WHERE message_id = @messageId";

            using (var connection = new SqlConnection(_config.GetConnectionString("IHMSConnection")))
            {
                var results = await connection.QueryAsync<MessageBoardDetailQuery>(sql, new { messageId });
                return Ok(results);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MessageBoardDetailQuery messageDetail)
        {
            // 直接在後端取得當前時間
            messageDetail.time = DateTime.Now;

            var sql = @"INSERT INTO [dbo].[message board details](message_id, Contents, member_id, time) 
                VALUES(@message_id, @Contents, @member_id, @time)";

            using (var connection = new SqlConnection(_config.GetConnectionString("IHMSConnection")))
            {
                var results = await connection.ExecuteAsync(sql, messageDetail);
                if (results > 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
        }




    }
}
