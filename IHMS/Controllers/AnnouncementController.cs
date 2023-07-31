using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using IHMS.Models;
using Microsoft.Data.SqlClient;

namespace IHMS.Controllers
{
    public class AnnouncementController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult testss()
        {
            return View();
        }

        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;

        public AnnouncementController(IWebHostEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            _configuration = configuration;

        }
        [HttpPost]
        public IActionResult Create(AnnouncementView model, IFormFile imageFile)
        {
            string connectionString = _configuration.GetConnectionString("IHMSConnection");
            model.time = DateTime.Now;

                model.time = DateTime.Now;

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var parameters = new
                    {
                        Title = model.title,
                        Content = model.contents,
                        CreatedDate = model.time,
                        Image = string.Empty // 先將圖片欄位設為空，稍後會更新為實際的檔案名稱
                    };

                }

                if (imageFile != null && imageFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_environment.WebRootPath, "AnnouncementImage");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }

                    // 更新公告的圖片欄位為實際的檔案名稱
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        var updateQuery = "UPDATE Announcement SET image = @Image WHERE title = @Title";
                        var updateParameters = new
                        {
                            Image = uniqueFileName,
                            Title = model.title
                        };
                        connection.Execute(updateQuery, updateParameters);
                    }
                }

                TempData["ShowPopup"] = true;

                return RedirectToAction("Index", "Announcement");
            

        }

        public IActionResult PastAnnouncements()
        {
            string connectionString = _configuration.GetConnectionString("IHMSConnection");
            IEnumerable<AnnouncementView> pastAnnouncements;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Announcement ORDER BY time DESC";
                pastAnnouncements = connection.Query<AnnouncementView>(query);
            }
              
            return View("PastAnnouncements", pastAnnouncements);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            string connectionString = _configuration.GetConnectionString("IHMSConnection");
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "DELETE FROM Announcement WHERE announcemet_id = @Id";
                var parameters = new { Id = id };
                connection.Execute(query, parameters);
            }
            return RedirectToAction("PastAnnouncements");
        }

    }
}
