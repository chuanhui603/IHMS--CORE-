using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;

namespace IHMS.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AnnouncementsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/Announcements
        [HttpGet]
        public IEnumerable<Announcement> Get()
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string query = "SELECT * FROM Announcement";
                return connection.Query<Announcement>(query);
            }
        }

        // GET: api/Announcements/5
        [HttpGet("{id}")]
        public ActionResult<Announcement> Get(int id)
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string query = "SELECT * FROM Announcement WHERE announcemet_id = @Id";
                var parameters = new { Id = id };
                var announcement = connection.QueryFirstOrDefault<Announcement>(query, parameters);
                if (announcement == null)
                {
                    return NotFound();
                }
                return announcement;
            }
        }

        // POST: api/Announcements
        [HttpPost]
        public ActionResult<Announcement> Post([FromBody] Announcement announcement)
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string query = @"INSERT INTO Announcement (title, contents, time, image)
                                 VALUES (@Title, @Contents, @Time, @Image);
                                 SELECT CAST(SCOPE_IDENTITY() as int)";
                var parameters = new
                {
                    Title = announcement.Title,
                    Contents = announcement.Contents,
                    Time = announcement.Time,
                    Image = announcement.Image
                };
                int id = connection.QuerySingle<int>(query, parameters);
                announcement.announcemet_id = id;
                return CreatedAtAction(nameof(Get), new { id = announcement.announcemet_id }, announcement);
            }
        }

        // PUT: api/Announcements/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Announcement announcement)
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string query = @"UPDATE Announcement
                                 SET title = @Title, contents = @Contents, time = @Time, image = @Image
                                 WHERE announcemet_id = @Id";
                announcement.announcemet_id = id;
                connection.Execute(query, announcement);
                return NoContent();
            }
        }

        // DELETE: api/Announcements/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string query = "DELETE FROM Announcement WHERE announcemet_id = @Id";
                var parameters = new { Id = id };
                connection.Execute(query, parameters);
                return NoContent();
            }
        }
    }

    public class Announcement
    {
        [Column("announcemet_id")]
        public int announcemet_id { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public DateTime Time { get; set; }
        public string Image { get; set; }
    }
}
