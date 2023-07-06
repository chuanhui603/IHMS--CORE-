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
            var planlist = (from p in db.Plans select p).ToList();      //不ToList會觸發重複使用資料庫    
            foreach (var p in planlist)
            {
                PPlanListViewModel plan = new PPlanListViewModel();
                plan.PlanId = p.PlanId;
                plan.Name = db.Members.Include("Plans").Where(m => m.MMemberId.Equals(p.MemberId)).FirstOrDefault().MName;
                plan.Registerdate =p.RegisterDate;
                plan.EndDate = p.EndDate;
                list.Add(plan);
            }
            return View(list);
        }
    
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {

                Plan cust = db.Plans.FirstOrDefault(p => p.PlanId == id);
                if (cust != null)
                {
                    db.Plans.Remove(cust);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("List");
        }
        public ActionResult Edit(int? id)
        {
            PPlanViewModel cust = new PPlanViewModel();

            if (id == null)
            {
                return RedirectToAction("List");
            }
           
            
            return View(cust);
        }
        [HttpPost]
        public ActionResult Edit(Plan t)
        {
            Plan cust = db.Plans.FirstOrDefault(p => p.PlanId == t.PlanId);
            if (cust != null)
            {
                cust.Weight = t.Weight;
                cust.BodyPercentage = t.BodyPercentage;
                cust.RegisterDate = t.RegisterDate;
                cust.EndDate = t.EndDate;
                db.SaveChanges();
            }

            return RedirectToAction("List");
        }

    }
}
