using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace IHMS.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomServiceController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public CustomServiceController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public class CustomerService
        {
            public int customer_service_id { get; set; }
            public int member_id { get; set; }
            public string Title { get; set; }
            public string Contents { get; set; }
            public string? reply { get; set; }
            public DateTime created_time { get; set; }
            public DateTime updated_time { get; set; }
        }

        // GET: api/CustomService
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("IHMSConnection")))
            {
                var tickets = await connection.QueryAsync<CustomerService>("SELECT * FROM CustomerService");
                return Ok(tickets.ToList());
            }
        }

        // GET: api/CustomService/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("IHMSConnection")))
            {
                var ticket = await connection.QuerySingleOrDefaultAsync<CustomerService>("SELECT * FROM CustomerService WHERE customer_service_id = @id", new { id });

                if (ticket == null)
                {
                    return NotFound();
                }

                return Ok(ticket);
            }
        }

        // POST: api/CustomService
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerService newTicket)
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("IHMSConnection")))
            {
                newTicket.created_time = DateTime.Now;
                newTicket.updated_time = DateTime.Now;
                var sql = "INSERT INTO CustomerService (member_id, Title, Contents, created_time, updated_time) VALUES (@member_id, @Title, @Contents, @created_time, @updated_time); SELECT CAST(SCOPE_IDENTITY() as int);";
                var id = await connection.QuerySingleAsync<int>(sql, newTicket);

                return CreatedAtAction(nameof(Get), new { id }, newTicket);
            }
        }

        // PUT: api/CustomService/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CustomerService updatedTicket)
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("IHMSConnection")))
            {
                if (id != updatedTicket.customer_service_id)
                {
                    return BadRequest();
                }

                updatedTicket.updated_time = DateTime.Now;
                var sql = "UPDATE CustomerService SET member_id = @member_id, Title = @Title, Contents = @Contents, Reply = @Reply, created_time = @created_time, updated_time = @updated_time WHERE customer_service_id = @customer_service_id";
                await connection.ExecuteAsync(sql, updatedTicket);

                return NoContent();
            }
        }

        // DELETE: api/CustomService/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("IHMSConnection")))
            {
                var sql = "DELETE FROM CustomerService WHERE customer_service_id = @id";
                var rows = await connection.ExecuteAsync(sql, new { id });

                if (rows == 0)
                {
                    return NotFound();
                }

                return NoContent();
            }
        }
    }
}
