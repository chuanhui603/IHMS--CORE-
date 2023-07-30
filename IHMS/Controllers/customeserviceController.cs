using IHMS.Models;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
namespace IHMS.Controllers
{
    public class customeserviceController : Controller
    {
        private readonly IConfiguration _configuration;
        public customeserviceController( IConfiguration configuration)
        {
           
            _configuration = configuration;

        }
        public IActionResult Index()
        {
            string connectionString = _configuration.GetConnectionString("IHMSConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var customerServiceReports = connection.Query<CustomerServiceReport>("SELECT * FROM CustomerService ORDER BY updated_time DESC");

                return View(customerServiceReports);
            }
        }
        public IActionResult Reply(int id)
        {
            string connectionString = _configuration.GetConnectionString("IHMSConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var report = connection.QueryFirstOrDefault<CustomerServiceReport>("SELECT * FROM CustomerService WHERE customer_service_id = @Id", new { Id = id });

                if (report == null)
                {
                    return NotFound(); // 如果找不到對應的回報，返回 404
                }

                return View(report);
            }
        }
        [HttpPost]
        public IActionResult SubmitReply(int id, string reply)
        {
            string connectionString = _configuration.GetConnectionString("IHMSConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                connection.Execute("UPDATE CustomerService SET reply = @Reply, updated_time = @UpdatedTime WHERE customer_service_id = @Id", new { Reply = reply, UpdatedTime = DateTime.Now, Id = id });
            }

            return RedirectToAction("Index");
        }
    }
}
