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
        

        // GET: Coach
        public ActionResult List_Done()
        {
            IhmsContext db = new IhmsContext();

            
            List<CCoachResumeViewModel> coaches = new List<CCoachResumeViewModel>();
            foreach (var c in db.Coaches.OrderByDescending(c => c.ApplyDate))
            {
                CCoachResumeViewModel coachViewModel = new CCoachResumeViewModel(db)
                {
                    CoachId = c.CoachId,
                    CoachName = c.CoachName,
                    MemberId = c.MemberId,
                    CityId = c.CityId,
                    CoachImage = c.CoachImage,
                    CoachFee = c.CoachFee,
                    CoachDescription = c.CoachDescription,
                    ApplyDate = c.ApplyDate,
                    StatusNumber = c.StatusNumber,
                    Visible = c.Visible,
                    Slogan = c.Slogan
                };
                coaches.Add(coachViewModel);
            }
            return View(coaches);
        }
        public IActionResult getCoach(int? id)
        {
            var c = db.Coaches.FirstOrDefault(c => c.CoachId == id);
            CCoachResumeViewModel coachViewModel = new CCoachResumeViewModel(db)
            {
                CoachId = c.CoachId,
                CoachName = c.CoachName,
                MemberId = c.MemberId,
                CityId = c.CityId,
                CoachImage = c.CoachImage,
                CoachFee = c.CoachFee,
                CoachDescription = c.CoachDescription,
                ApplyDate = c.ApplyDate,
                StatusNumber = c.StatusNumber,
                Visible = c.Visible,
                Slogan = c.Slogan
            };

            return Json(coachViewModel);
        }

        public IActionResult passResume(int? id)
        {
            var theCoach = db.Coaches.FirstOrDefault(c => c.CoachId == id);
            theCoach.StatusNumber = 2;
            theCoach.Visible = true;
            db.SaveChanges();
            return Content("");
        }
        public IActionResult returnResume(int? id)
        {
            var theCoach = db.Coaches.FirstOrDefault(c => c.CoachId == id);
            theCoach.StatusNumber = 3;

            db.SaveChanges();
            return Content("");
        }

        public IActionResult loadCoach(CSearchCoachViewModel searchCoachViewModel)
        {
            IEnumerable<Coach> tCoaches = null;
            if (searchCoachViewModel.SkillId != 0)
                tCoaches = db.CoachSkills.Where(cs => cs.SkillId == searchCoachViewModel.SkillId).Select(cs => cs.Coach);
            else
                tCoaches = db.Coaches;

            if (searchCoachViewModel.Sort == 1)
                tCoaches = tCoaches.OrderByDescending(c => c.ApplyDate);
            else
                tCoaches = tCoaches.OrderBy(c => c.ApplyDate);

            if (searchCoachViewModel.CityId != 0)
                tCoaches = tCoaches.Where(c => c.CityId == searchCoachViewModel.CityId);
            if (!String.IsNullOrEmpty(searchCoachViewModel.KeyWord))
                tCoaches = tCoaches.Where(c => c.CoachName.ToLower()
                .Contains(searchCoachViewModel.KeyWord.ToLower()) || c.CoachId.ToString().Contains(searchCoachViewModel.KeyWord));
            if (searchCoachViewModel.StatusNum != 0)
                tCoaches = tCoaches.Where(c => c.StatusNumber == searchCoachViewModel.StatusNum);

            List<CCoachResumeViewModel> coaches = null;
            if (tCoaches.Count() != 0)
            {
                coaches = new List<CCoachResumeViewModel>();
                foreach (var c in tCoaches)
                {
                    CCoachResumeViewModel coachViewModel = new CCoachResumeViewModel(db)
                    {
                        CoachId = c.CoachId,
                        CoachName = c.CoachName,
                        MemberId = c.MemberId,
                        CityId = c.CityId,
                        CoachImage = c.CoachImage,
                        CoachFee = c.CoachFee,
                        CoachDescription = c.CoachDescription,
                        ApplyDate = c.ApplyDate,
                        StatusNumber = c.StatusNumber,
                        Visible = c.Visible,
                        Slogan = c.Slogan
                    };
                    coaches.Add(coachViewModel);
                }
            }
            return Json(coaches);
        }

        
        

        
    }
}
