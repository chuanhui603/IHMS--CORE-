using Microsoft.AspNetCore.Mvc;

namespace IHMS.Controllers
{
    public class SportController1 : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
