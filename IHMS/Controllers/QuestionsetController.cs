using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using IHMS.Models;
using Microsoft.Data.SqlClient;

namespace IHMS.Controllers
{
    public class QuestionsetController : Controller
    {
        private readonly IConfiguration _configuration;

        public QuestionsetController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<Questionset> questionsets = connection.Query<Questionset>("SELECT * FROM Questionset").AsList();
                return View(questionsets);
            }
        }
        [HttpPost]
        public IActionResult Create(Questionset questionset)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                // 在此執行新增資料的邏輯，例如使用 Dapper 執行 INSERT INTO 語句
                // 使用 questionset.q_question 和 questionset.q_category 存取屬性值

                // 範例：
                string query = "INSERT INTO Questionset (q_question, q_category) VALUES (@Question, @Category)";
                connection.Execute(query, new { Question = questionset.QQuestion, Category = questionset.QCategory });
            }

            // 新增完成後重新導向至 Index 頁面
            return RedirectToAction("Index");
        }
    }
}