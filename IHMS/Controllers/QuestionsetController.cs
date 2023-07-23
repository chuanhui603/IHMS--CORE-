using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Data.SqlClient;
using IHMS.Models;

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
            string connectionString = _configuration.GetConnectionString("IHMSConnection");

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<QuestionsetViewModel> questionsets = connection.Query<QuestionsetViewModel>("SELECT * FROM Questionset").AsList();
                return View(questionsets);
            }
        }
        [HttpPost]
        public IActionResult Create(QuestionsetViewModel questionset)
        {
            string connectionString = _configuration.GetConnectionString("IHMSConnection");

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                // 在此執行新增資料的邏輯，例如使用 Dapper 執行 INSERT INTO 語句
                // 使用 questionset.question 和 questionset.category 存取屬性值

                // 範例：
                string query = "INSERT INTO Questionset (question, category) VALUES (@Question, @Category)";
                var parameters = new DynamicParameters();
                parameters.Add("@Question", questionset.question);
                parameters.Add("@Category", questionset.category);
                connection.Execute(query, parameters);
            }

            // 新增完成後重新導向至 Index 頁面
            return RedirectToAction("Index");
        }
        public IActionResult CreateQuestion()
        {
            return View();
        }

       
        [HttpGet]
        public IActionResult Edit(int id)
        {
            string connectionString = _configuration.GetConnectionString("IHMSConnection");

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                // 根據問題的唯一識別碼（Id）從資料庫中獲取問題詳細資訊
                QuestionsetViewModel questionset = connection.QueryFirstOrDefault<QuestionsetViewModel>("SELECT * FROM Questionset WHERE questionset_id = @Id", new { Id = id });

                if (questionset == null)
                {
                    return NotFound();
                }

                return View(questionset);
            }
        }


        [HttpPost]
        public IActionResult Edit(QuestionsetViewModel questionset)
        {

                string connectionString = _configuration.GetConnectionString("IHMSConnection");

                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    // 在此執行更新資料的邏輯，例如使用 Dapper 執行 UPDATE 語句
                    // 使用 questionset.questionset_id, questionset.question 和 questionset.category 存取屬性值

                    // 範例：
                    string query = "UPDATE Questionset SET question = @Question, category = @Category WHERE questionset_id = @Id";
                    var parameters = new DynamicParameters();
                    parameters.Add("@Id", questionset.questionset_id);
                    parameters.Add("@Question", questionset.question);
                    parameters.Add("@Category", questionset.category);
                    connection.Execute(query, parameters);
                }

                // 更新完成後重新導向至 Index 頁面
                return RedirectToAction("Index");
            

        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            string connectionString = _configuration.GetConnectionString("IHMSConnection");

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                // 在此執行刪除資料的邏輯，例如使用 Dapper 執行 DELETE 語句
                // 使用 id 參數作為刪除條件

                // 範例：
                string query = "DELETE FROM Questionset WHERE questionset_id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                connection.Execute(query, parameters);
            }

            // 刪除完成後重新導向至 Index 頁面
            return RedirectToAction("Index");
        }

    }
}
