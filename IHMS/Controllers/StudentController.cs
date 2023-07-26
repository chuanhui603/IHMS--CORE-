using Microsoft.AspNetCore.Mvc;

namespace IHMS.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
