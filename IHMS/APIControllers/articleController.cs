using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace IHMS.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ArticleController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection Connection
        {
            get
            {
                string connectionString = _configuration.GetConnectionString("IHMSConnection");
                return new SqlConnection(connectionString);
            }
        }

        public class Article
        {
            public int Article_id { get; set; }
            public string Title { get; set; }
            public string Contents { get; set; }
            public DateTime Time { get; set; }
            public string Image { get; set; } // for storing single image URL in Get()
            public List<string> Images { get; set; } // for storing multiple image URLs in Get(id)
        }

        // GET: api/article
        [HttpGet]
        public async Task<IEnumerable<Article>> Get()
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT * FROM article";
                conn.Open();
                var result = await conn.QueryAsync<Article>(sQuery);

                // For each article, get the first image
                foreach (var article in result)
                {
                    string imageQuery = "SELECT TOP(1) image FROM article_image WHERE article_id = @ID";
                    var image = await conn.QueryFirstOrDefaultAsync<string>(imageQuery, new { ID = article.Article_id });
                    if (image != null)
                    {
                        article.Image = image;
                    }
                    else
                    {
                        Console.WriteLine($"No image found for article ID: {article.Article_id}");
                    }
                }

                return result;
            }
        }

        // GET: api/article/{id}
        [HttpGet("{id}")]
        public async Task<Article> Get(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT * FROM article WHERE article_id = @ID";
                conn.Open();
                var result = await conn.QueryFirstOrDefaultAsync<Article>(sQuery, new { ID = id });

                string imageQuery = "SELECT image FROM article_image WHERE article_id = @ID";
                var images = await conn.QueryAsync<string>(imageQuery, new { ID = id });

                if (result != null)
                    result.Images = images.ToList(); // store all image URLs

                return result;
            }
        }
    }
}
