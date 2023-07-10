using IHMS.Models;
using IHMS.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Numerics;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace IHMS.Controllers
{
    public class PlanController : Controller
    {
        IhmsContext db;  
        
        public PlanController(IhmsContext d)
        {
            db = d;
        }
        public ActionResult List()
        {
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
            List<PPlanListViewModel> list = new List<PPlanListViewModel>();
            var query = db.Plans.Select(p => new PPlanListViewModel
            {
                PlanId = p.PlanId,
                Name = db.Members.Include("Plans").FirstOrDefault(m => m.MMemberId==p.MemberId).MName,
                Registerdate = p.RegisterDate,
                EndDate = p.EndDate,
            });
            return View(query);
        }

        public ActionResult Delete(int? id)
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
            if (id == null)
            {
                return RedirectToAction("List");
            }
            var plan = db.Plans.FirstOrDefault(p => p.PlanId == id);
            var dietquery = db.Diets.Include("Plan").Where(d => d.PlanId.Equals(plan.PlanId)).ToList();
            var sportquery = db.Sports.Include("Plan").Where(s => s.PlanId.Equals(plan.PlanId)).ToList();
            var dietdatelist = new List<DateTime>();
            var dietId = new List<int>();
            var sportdatelist = new List<DateTime>();
            var sportId = new List<int>();
            foreach (var diet in dietquery)
            {
                dietdatelist.Add(diet.Date);
                dietId.Add(diet.DietId);
            }

            foreach (var sport in sportquery)
            {
                sportdatelist.Add(sport.Date);
                sportId.Add(sport.SportId);
            }
            PPlanViewModel vm = new PPlanViewModel
            {
                PlanId = plan.PlanId,
                MemberName = db.Members.Include("Plans").FirstOrDefault(m => m.MMemberId == plan.MemberId).MName,
                BodyPercentage = plan.BodyPercentage,
                RegisterDate = plan.RegisterDate,
                EndDate = plan.EndDate,
                Pname = plan.Pname,
                DietDate = dietdatelist,
                DietId = dietId,
                SportDate = sportdatelist,
                SportId = sportId,
            };                 
            return View(vm);
        }

        [HttpPost]
        public IActionResult Detail(int? id,string type)
        {
            switch (type){
                case "sport":
                    return RedirectToAction("Details", "Diet", new {id = id});                
                default:
                    return RedirectToAction("Details", "Sport", new { id = id });                   
            }
               
        }
    }
}
