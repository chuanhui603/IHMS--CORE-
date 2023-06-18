using IHMS.Models;
using IHMS.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace Final.Controllers
{
    public class ShoppingController : Controller
    {
        // GET: ShoppingController
        public IActionResult List(CKeywordViewModel vm)
        {
            string keyword = vm.txtKeyword;
            IhmsContext db = new IhmsContext();
            IEnumerable<CourseOrder> datas = null;
            if (string.IsNullOrEmpty(keyword))
            {
                datas = from p in db.CourseOrders
                        select p;

            }
            else
            {
                datas = db.CourseOrders.Where(p => p.CoState.Contains(keyword));
                //p.CoPointstotal.Contains(keyword) ||
                //p.FPhone.Contains(keyword) ||
                //p.CoMemberId.Contains(keyword) || ;
            }

            datas = datas.OrderBy(p => p.CoCreatetime);
            return View(datas);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            IhmsContext db = new IhmsContext();
            CourseOrder cust = db.CourseOrders.FirstOrDefault(p => p.CoCourseorderId == id);
            return View(cust);
        }
        [HttpPost]
        public ActionResult Edit(CourseOrder x)
        {
            if (x.CoState == "已取消" && string.IsNullOrWhiteSpace(x.CoReason))
            {
                ModelState.AddModelError("CoReason", "當選擇已取消時，請填寫原因");
                return View();
            }
            IhmsContext db = new IhmsContext();
            CourseOrder cust = db.CourseOrders.FirstOrDefault(p => p.CoCourseorderId == x.CoCourseorderId);
            if (cust != null)
            {
                cust.CoMemberId= x.CoMemberId;
                cust.CoPointstotal = x.CoPointstotal;
                cust.CoState = x.CoState;                
                cust.CoReason = x.CoReason;
                cust.CoCreatetime = DateTime.Now;
                cust.CoUpdatetime = DateTime.Now;
                

                db.SaveChanges();
            }
            return RedirectToAction("List");
        }



       
    }
}
