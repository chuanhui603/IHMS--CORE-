using Microsoft.AspNetCore.Mvc;

namespace IHMS.Controllers
{
    public class PlanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
