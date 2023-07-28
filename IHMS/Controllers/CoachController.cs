using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IHMS.Models;
using IHMS.ViewModel;

namespace IHMS.Controllers
{

    public class CoachController : Controller
    {
        private IhmsContext db;
        public CoachController(IhmsContext _context)
        {
            db = _context;
        }
        //private IWebHostEnvironment _enviro = null;

        //public CoachController(IWebHostEnvironment e)
        //{
        //    _enviro = e;
        //}

        // GET: Coach
        public ActionResult List_Done()
        {
            
            IhmsContext db = new IhmsContext();
            //foreach (var c in db.Coaches.OrderByDescending(c => c.Applytime))
            //{
               var datas = db.Coaches.Include("Member").Select(c => c.Condition == 1 || c.Condition == 0 || c.Condition == 2);
            //}
            return View(datas);
            //List<CCoachResumeViewModel> coaches = new List<CCoachResumeViewModel>();
            //foreach (var c in db.Coaches.OrderByDescending(c => c.Applytime))
            //{
            //    CCoachResumeViewModel coachViewModel = new CCoachResumeViewModel(db)
            //    {
            //        CoachId = c.CoachId,
            //        MemberId = c.MemberId,
            //        Image = c.Image,
            //        Intro = c.Intro,
            //        Rank = c.Rank,
            //        Commission = c.Commission,
            //        Condition = c.Condition.ToString(),
            //        Reason = c.Reason,
            //        Resume = c.Resume,
            //        Type = c.Type,
            //        Applytime = c.Applytime,
            //        Confirmtime = c.Confirmtime,
            //        Video = c.Video
                    
            //    };
            //    coaches.Add(coachViewModel);
            //}
            //return View(coaches);
        }
        public IActionResult getCoach(int? id)
        {
            var c = db.Coaches.FirstOrDefault(c => c.CoachId == id);
            CCoachResumeViewModel coachViewModel = new CCoachResumeViewModel(db)
            {
                CoachId = c.CoachId,
                MemberId = c.MemberId,
                Image = c.Image,
                Intro = c.Intro,
                Rank = c.Rank,
                Commission = c.Commission,
                Condition = c.Condition.ToString(),
                Reason = c.Reason,
                Resume = c.Resume,
                Type = c.Type,
                Applytime = c.Applytime,
                Confirmtime = c.Confirmtime,
                Video = c.Video
            };

            return Json(coachViewModel);
        }

        public IActionResult passResume(int? id)
        {
            var theCoach = db.Coaches.FirstOrDefault(c => c.CoachId == id);
            theCoach.Condition = 1;
            
            db.SaveChanges();
            return Content("");
        }
        public IActionResult returnResume(int? id)
        {
            var theCoach = db.Coaches.FirstOrDefault(c => c.CoachId == id);
            theCoach.Condition = 2;

            db.SaveChanges();
            return Content("");
        }

        public IActionResult loadCoach(CSearchCoachViewModel searchCoachViewModel)
        {
            IEnumerable<Coach> tCoaches = null;
           
                tCoaches = db.Coaches;

            if (searchCoachViewModel.Sort == 1)
                tCoaches = tCoaches.OrderByDescending(c => c.Applytime);
            else
                tCoaches = tCoaches.OrderBy(c => c.Applytime);

            
            if (!String.IsNullOrEmpty(searchCoachViewModel.KeyWord))
                tCoaches = tCoaches.Where(c => c.Member.Name.ToLower().Contains(searchCoachViewModel.KeyWord.ToLower()) || c.CoachId.ToString().Contains(searchCoachViewModel.KeyWord));
            if (searchCoachViewModel.Condition != null)
                tCoaches = tCoaches.Where(c => c.Condition == searchCoachViewModel.Condition);

            List<CCoachResumeViewModel> coaches = null;
            if (tCoaches.Count() != 0)
            {
                coaches = new List<CCoachResumeViewModel>();
                foreach (var c in tCoaches)
                {
                    CCoachResumeViewModel coachViewModel = new CCoachResumeViewModel(db)
                    {
                        CoachId = c.CoachId,
                        MemberId = c.MemberId,
                        Image = c.Image,
                        Intro = c.Intro,
                        Rank = c.Rank,
                        Commission = c.Commission,
                        Condition = c.Condition.ToString(),
                        Reason = c.Reason,
                        Resume = c.Resume,
                        Type = c.Type,
                        Applytime = c.Applytime,
                        Confirmtime = c.Confirmtime,
                        Video = c.Video
                    };
                    coaches.Add(coachViewModel);
                }
            }
            return Json(coaches);
        }
        //public ActionResult List_UnReviewed()
        //{
        //    //IhmsContext db = new IhmsContext();
        //    //var datas = from c in db.Coaches
        //    //            where c.Condition == 0 ||c.Condition == null
        //    //            select c;
        //    //return View(datas);
        //}
        public ActionResult List_Rejected()
        {
            IhmsContext db = new IhmsContext();
            var datas = from c in db.Coaches
                        where c.Condition == 2
                        select c;
            return View(datas);
        }
        public ActionResult List_resigned()
        {
            IhmsContext db = new IhmsContext();
            var datas = from c in db.Coaches
                        where c.Condition == 3
                        select c;
            return View(datas);
        }

        // GET: Coach/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
            //if (id == null || _context.Coaches == null)
            //{
            //    return NotFound();
            //}

            //var coach = await _context.Coaches
            //    .Include(c => c.Member)
            //    .FirstOrDefaultAsync(m => m.CoachId == id);
            //if (coach == null)
            //{
            //    return NotFound();
            //}

            //return View(coach);
        //}

        // GET: Coach/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: Coach/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Coach c)
        //{
        //    IhmsContext db = new IhmsContext();
        //    db.Coaches.Add(c);
        //    db.SaveChanges();
        //    return RedirectToAction("List_Done");
        //}

        // GET: Coach/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    if (id == null)
        //        return RedirectToAction("List_Done");
        //    IhmsContext db = new IhmsContext();
        //    Coach coach = db.Coaches.FirstOrDefault(c => c.CoachId == id);
        //    return View(coach);
        //}

        // POST: Coach/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(CCoachWrapper x)
        //{
        //    IhmsContext db = new IhmsContext();
        //    Coach coach = db.Coaches.FirstOrDefault(c => c.CoachId == x.CoachId);

        //    if (coach != null)
        //    {
        //        if (x.photo != null)
        //        {
        //            string photoName = Guid.NewGuid().ToString() + ".jpg";
        //            x.photo.CopyTo(new FileStream(
        //                _enviro.WebRootPath + "/images/" + photoName,
        //                FileMode.Create));
        //            coach.Image = photoName;
        //        }
        //        coach.Intro = x.Intro;
        //        coach.Video = x.Video;
        //        coach.Resume = x.Resume;
        //        coach.Applytime = DateTime.Now;

        //        db.SaveChanges();
        //    }
        //    return RedirectToAction("List_Done");
        //}

        // GET: Coach/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
            //if (id == null || _context.Coaches == null)
            //{
            //    return NotFound();
            //}

            //var coach = await _context.Coaches
            //    .Include(c => c.Member)
            //    .FirstOrDefaultAsync(m => m.CoachId == id);
            //if (coach == null)
            //{
            //    return NotFound();
            //}

            //return View(coach);
        //}

        // POST: Coach/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        ////public async Task<IActionResult> DeleteConfirmed(int id)
        //{
            //if (_context.Coaches == null)
            //{
            //    return Problem("Entity set 'IhmsContext.Coaches'  is null.");
            //}
            //var coach = await _context.Coaches.FindAsync(id);
            //if (coach != null)
            //{
            //    _context.Coaches.Remove(coach);
            //}
            
            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
        //}

        //private bool CoachExists(int id)
        //{
        //  return (_context.Coaches?.Any(e => e.CoachId == id)).GetValueOrDefault();
        //}
    }
}
