using IHMS.Models;
using IHMS.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace IHMS.Controllers
{
    public class PlanController : Controller
    {
        IhmsContext db = new IhmsContext();


        public IActionResult List()
        {
            List<PPlanListViewModel> list = new List<PPlanListViewModel>();
            // var planlist = (from p in db.Plans select p);      //不ToList會觸發重複使用資料庫         
            //foreach (var p in planlist)
            //{
            //    PPlanListViewModel plan = new PPlanListViewModel();
            //    plan.PlanId = p.PlanId;
            //    plan.Name = db.Members.Include("Plans").FirstOrDefault(m => m.MMemberId.Equals(p.MemberId)).MName;
            //    plan.Registerdate =p.RegisterDate;
            //    plan.EndDate = p.EndDate;
            //    list.Add(plan);
            //}
            var query = db.Plans.Select(p => new PPlanListViewModel
            {
                PlanId = p.PlanId,
                Name = db.Members.Include("Plans").FirstOrDefault(m => m.MMemberId.Equals(p.MemberId)).MName,
                Registerdate = p.RegisterDate,
                EndDate = p.EndDate,
            }); ;
            return View(query);
        }

        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                Plan plan = db.Plans.FirstOrDefault(p => p.PlanId == id);
                if (plan != null)
                {
                    db.Plans.Remove(plan);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("List");
        }
        public ActionResult Detail(int? id)
        {
            PPlanViewModel vm = new PPlanViewModel();

            if (id == null)
            {
                return RedirectToAction("List");
            }
            var plan = db.Plans.FirstOrDefault(p => p.PlanId == id);
            vm.PlanId = plan.PlanId;
            vm.MemberName = db.Members.Include("Plans").FirstOrDefault(m => m.MMemberId.Equals(plan.MemberId)).MName;
            vm.BodyPercentage = plan.BodyPercentage;
            vm.RegisterDate = plan.RegisterDate;
            vm.EndDate = plan.EndDate;
            vm.Pname = plan.Pname;
            return View(vm);
        }

    }
}
