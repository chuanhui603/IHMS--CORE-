using IHMS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace IHMS.Controllers
{
    public class PlanController : Controller
    {
        public IActionResult List( )
        {
            IhmsContext db = new IhmsContext();
            IEnumerable<Plan> datas = null;     
                datas = from p in db.Plans select p;         
            return View(datas);
        }
    
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                IhmsContext db = new IhmsContext();
                Member cust = db.Members.FirstOrDefault(p => p.MMemberId == id);
                if (cust != null)
                {
                    db.Members.Remove(cust);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("List");
        }
        public ActionResult Edit(int? id)
        {
            Member cust = new Member();

            if (id == null)
            {
                return RedirectToAction("List");
            }
            IhmsContext db = new IhmsContext();
            cust = db.Members.FirstOrDefault(p => p.MMemberId == id);
            return View(cust);
        }
        [HttpPost]
        public ActionResult Edit(Plan t)
        {
            IhmsContext db = new IhmsContext();
            Plan cust = db.Plans.FirstOrDefault(p => p.PlanId == t.PlanId);
            if (cust != null)
            {
                cust.Weight = t.Weight;
                cust.BodyPercentage = t.BodyPercentage;
                cust.Registerdate = t.Registerdate;
                cust.EndDate = t.EndDate;
                db.SaveChanges();
            }

            return RedirectToAction("List");
        }

    }
}
