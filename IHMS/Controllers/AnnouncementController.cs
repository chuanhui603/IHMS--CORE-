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

        private readonly string connectionString = "Data Source=.;Initial Catalog=IHMS;Integrated Security=True;TrustServerCertificate=True"; // 替換為你的資料庫連接字串
        private readonly IWebHostEnvironment _environment;

        public AnnouncementController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        [HttpPost]
        public IActionResult Create(AnnouncementView model, IFormFile imageFile)
        {

                model.an_time = DateTime.Now;

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var query = "INSERT INTO Announcement (an_title, an_content, an_time, an_image) VALUES (@Title, @Content, @CreatedDate, @Image)";
                    var parameters = new
                    {
                        Title = model.an_title,
                        Content = model.an_content,
                        CreatedDate = model.an_time,
                        Image = string.Empty // 先將圖片欄位設為空，稍後會更新為實際的檔案名稱
                    };
                    connection.Execute(query, parameters);
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
                        var updateQuery = "UPDATE Announcement SET an_image = @Image WHERE an_title = @Title";
                        var updateParameters = new
                        {
                            Image = uniqueFileName,
                            Title = model.an_title
                        };
                        connection.Execute(updateQuery, updateParameters);
                    }
                }

                TempData["ShowPopup"] = true;

                return RedirectToAction("Index", "Announcement");
            

        }

        public IActionResult PastAnnouncements()
        {
            IEnumerable<AnnouncementView> pastAnnouncements;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Announcement ORDER BY an_time DESC";
                pastAnnouncements = connection.Query<AnnouncementView>(query);
            }
              
            return View("PastAnnouncements", pastAnnouncements);
        }

    }
}
