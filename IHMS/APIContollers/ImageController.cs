using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace IHMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "AnnouncementImage", imageName);
            byte[] imageData = System.IO.File.ReadAllBytes(imagePath);
            return File(imageData, "image/jpeg");
        }
    }
}
