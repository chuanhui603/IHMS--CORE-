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
        IhmsContext _db;  
        
        public PlanController(IhmsContext d)
        {
            _db = d;
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
            var query = _db.Plans.Select(p => new PPlanListViewModel
            {
                PlanId = p.PlanId,
                Name = _db.Members.Include("Plans").FirstOrDefault(m => m.MemberId==p.MemberId).Name,
                Registerdate = p.RegisterDate,
                EndDate = p.EndDate,
            });
            return View(query);
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                Plan plan = _db.Plans.FirstOrDefault(p => p.PlanId == id);
                if (plan != null)
                {
                    _db.Plans.Remove(plan);
                    _db.SaveChanges();
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
            var plan = _db.Plans.FirstOrDefault(p => p.PlanId == id);
            var dietquery = _db.Diets.Include("Plan").Where(d => d.PlanId.Equals(plan.PlanId)).ToList();
            var sportquery = _db.Sports.Include("Plan").Where(s => s.PlanId.Equals(plan.PlanId)).ToList();
            var dietdatelist = new List<DateTime>();
            var dietId = new List<int>();
            var sportdatelist = new List<DateTime>();
            var sportId = new List<int>();
            foreach (var diet in dietquery)
            {
                dietdatelist.Add(diet.Createdate);
                dietId.Add(diet.DietId);
            }

            foreach (var sport in sportquery)
            {
                sportdatelist.Add(sport.Createdate);
                sportId.Add(sport.SportId);
            }
            PPlanViewModel vm = new PPlanViewModel
            {
                PlanId = plan.PlanId,
                MemberName = _db.Members.Include("Plans").FirstOrDefault(m => m.MemberId == plan.MemberId).Name,
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
