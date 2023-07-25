using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IHMS.Models;
using IHMS.ViewModel;
using IHMS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;


namespace IHMS.Controllers
{
    public class CoachFrontController : Controller
    {
        private IhmsContext db;
        private readonly IhmsContext _context;
        public CoachFrontController(IhmsContext context)
        {
            db = context;
            _context = context;
        }
        //教練列表
        public IActionResult CoachList(CKeywordViewModel v)
        {
            IEnumerable<Coach> datas = null;
            if (!String.IsNullOrEmpty(v.txtKeyword))
            {
                string keyword = v.txtKeyword.ToLower();
                datas = _context.Coaches
                    .Include(c => c.Member)
                    .Include(c => c.Courses)
                    
                    .Include(c => c.Intro)
                    .Include(c => c.Type)
                    .Include(c => c.Video)
                    .Where(c => 
                    (c.Member.Name.ToLower().Contains(keyword) || c.Member.ResidentialCity.ToLower().Contains(keyword)  ));
            }
            else
            {
                datas = _context.Coaches
                                    .Include(c => c.Member)
                    .Include(c => c.Courses)
                    
                    .Include(c => c.Intro)
                    .Include(c => c.Type)
                    .Include(c => c.Video)
                                    .Where(c => c.Condition== 1);
            }
            ViewBag.Keyword = v.txtKeyword;
            var coaches = CCoachViewModel.CoachList(datas.ToList());
            return View(coaches);
        }

        //教練詳細資料
        public IActionResult ViewCoachDetails(int? id)
        {
            IEnumerable<Coach> datas = null;
            Coach coach = _context.Coaches.FirstOrDefault(c => c.CoachId == id);
            if (coach == null)
                return RedirectToAction("CoachList");

            var data = _context.Coaches.Where(c => c.CoachId == id)
                .Include(c => c.Image)
                .Include(c => c.Courses)
                .Include(c => c.Intro)
                .Include(c => c.Type)
                .Include(c => c.Video);

            CCoachViewModel vModel = new CCoachViewModel
            {
                Coach = (Coach)data
            };
            return View(vModel);
        }
        

        //教練預約時間表-可預約
        public IActionResult getAvailableTimeId(int? id)
        {
            var ids = db.Schedules.Where(ca => ca.CourseId == id).Select(ca => ca.StartTime).Distinct();
            return Json(ids);
        }
        
        //通知教練
        public IActionResult createContact(Member Email)
        {
            //取得登入者ID
            
            return Content("");
        }


        

    }
}
