using IHMS.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace IHMS.Controllers
{
    public class DietController : Controller
    {
        public IActionResult Details(int id)
        {
            DietdetailViewModel vm = new DietdetailViewModel();
            vm.DietId = id;
            return View(vm);
        }
    }
}
