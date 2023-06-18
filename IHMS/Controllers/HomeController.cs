using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using IHMS.Models;
using Microsoft.Data.SqlClient;

namespace IHMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly string connectionString = "Data Source=.;Initial Catalog=IHMS;Integrated Security=True;TrustServerCertificate=True";
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            return View(); 
        }
        public IActionResult Index2()
        {
            var announcement = GetLatestAnnouncementFromDatabase();
            if (announcement != null)
            {
                TempData["ShowPopup"] = true;
                TempData["PopupContent"] = announcement;
            }

            return View(announcement);
        }
        private AnnouncementView GetLatestAnnouncementFromDatabase()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT TOP 1 an_title, an_content, an_image FROM Announcement ORDER BY an_time DESC";
                return connection.QuerySingleOrDefault<AnnouncementView>(query);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}