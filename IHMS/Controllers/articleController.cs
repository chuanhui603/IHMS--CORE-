using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using static IHMS.Controllers.Article;

namespace IHMS.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ArticleController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            string connectionString = _configuration.GetConnectionString("IHMSConnection");
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var articleDictionary = new Dictionary<int, Article>();

                var list = connection.Query<Article, ArticleImage, Article>(
                    "SELECT a.*, ai.image FROM article a LEFT JOIN article_image ai ON a.article_id = ai.article_id",
                    (article, articleImage) =>
                    {
                        Article articleEntry;

                        if (!articleDictionary.TryGetValue(article.article_id, out articleEntry))
                        {
                            articleEntry = article;
                            articleEntry.images = new List<string>();
                            articleDictionary.Add(articleEntry.article_id, articleEntry);
                        }

                        articleEntry.images.Add(articleImage.image);
                        return articleEntry;
                    },
                    splitOn: "image"
                ).Distinct().ToList();

                return View(list);
            }
        }

        [HttpPost]
        public IActionResult Create(Article article)
        {
            article.time = DateTime.Now;
            string connectionString = _configuration.GetConnectionString("IHMSConnection");
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO article (title, contents, time) VALUES (@Title, @Contents, @Time); SELECT CAST(SCOPE_IDENTITY() as int)";
                int articleId = connection.ExecuteScalar<int>(insertQuery, article);

                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                foreach (var file in Request.Form.Files)
                {
                    if (file != null && file.Length > 0)
                    {
                        string uniqueFileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        string insertImageQuery = "INSERT INTO article_image (article_id, image) VALUES (@ArticleId, @Image)";
                        connection.Execute(insertImageQuery, new { ArticleId = articleId, Image = "/uploads/" + uniqueFileName });
                    }
                }

                return RedirectToAction("Index");
            }
        }
        public IActionResult ViewAll()
        {
            string connectionString = _configuration.GetConnectionString("IHMSConnection");
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var articles = connection.Query<Article>(
                    "SELECT article_id, title FROM article"
                ).ToList();

                return View(articles);
            }
        }
        public IActionResult Details(int id)
        {
            string connectionString = _configuration.GetConnectionString("IHMSConnection");
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var articleDictionary = new Dictionary<int, Article>();

                var list = connection.Query<Article, ArticleImage, Article>(
                    "SELECT a.*, ai.image FROM article a LEFT JOIN article_image ai ON a.article_id = ai.article_id WHERE a.article_id = @Id",
                    (article, articleImage) =>
                    {
                        Article articleEntry;

                        if (!articleDictionary.TryGetValue(article.article_id, out articleEntry))
                        {
                            articleEntry = article;
                            articleEntry.images = new List<string>();
                            articleDictionary.Add(articleEntry.article_id, articleEntry);
                        }

                        articleEntry.images.Add(articleImage.image);
                        return articleEntry;
                    },
                    new { Id = id },
                    splitOn: "image"
                ).Distinct().ToList();

                if (list.Count == 0)
                {
                    return NotFound();
                }

                return View(list[0]);
            }
        }
    }


    public class Article
    {
        public int article_id { get; set; }
        public string title { get; set; }
        public string contents { get; set; }
        public DateTime time { get; set; }
        public List<string> images { get; set; }
    }
    public class ArticleImage
    {
        public int article_id { get; set; }
        public string image { get; set; }
    }
}
