using Microsoft.AspNetCore.Mvc;

namespace IHMS.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
