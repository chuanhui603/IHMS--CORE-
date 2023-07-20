using Microsoft.AspNetCore.Mvc;

namespace IHMS.Controllers
{
    public class SportController : Controller
    {
        public IActionResult Details()
        {
            return View();
        }
    }
}
