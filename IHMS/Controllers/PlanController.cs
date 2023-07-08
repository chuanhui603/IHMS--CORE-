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
        public IActionResult Detail(int? id)
        {                   
            if (id == null)
            {
                return RedirectToAction("List");
            }
            var plan = db.Plans.FirstOrDefault(p => p.PlanId == id);
            var dietquery = db.Diets.Include("Plans").Where(d => d.PlanId == plan.PlanId).ToList();
            var sportquery = db.Sports.Include("Plans").Where(s => s.PlanId == plan.PlanId).ToList();
            PPlanViewModel vm = new PPlanViewModel
            {
                PlanId = plan.PlanId,
                MemberName = db.Members.Include("Plans").FirstOrDefault(m => m.MMemberId == plan.MemberId).MName,
                BodyPercentage = plan.BodyPercentage,
                RegisterDate = plan.RegisterDate,
                EndDate = plan.EndDate,
                Pname = plan.Pname,
            };
            foreach (var diet in dietquery)
            {
                vm.DietDate.Add(diet.Date);
                vm.DietRegisterDate.Add(diet.Registerdate);
            }

            foreach (var sport in sportquery)
            {
                vm.DietDate.Add(sport.Date);
                vm.DietRegisterDate.Add(sport.Registerdate);
            }

            return View(vm);
        }
    }
}
