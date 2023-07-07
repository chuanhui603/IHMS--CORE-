using Microsoft.AspNetCore.Mvc;

namespace IHMS.Controllers
{
    public class DietController1 : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
