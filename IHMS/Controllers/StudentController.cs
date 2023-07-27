using IHMS.Models;
using IHMS.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using IHMS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IHMS.Controllers
{
    public class StudentController : Controller
    {
        private IhmsContext db;
        private readonly IhmsContext _context;
        public StudentController(IhmsContext context)
        {
            db = context;
            _context = context;
        }

        //教練列表
        public IActionResult CoachList(CKeywordViewModel v)
        {
            //IEnumerable<Coach> datas = null;

            //datas = _context.Coaches
            //                    .Include(c => c.Member)
            //                    .Include(c => c.CoachId)
            //                    .Include(c => c.Courses)
            //                    .Include(c => c.Image)
            //                    .Where(c => c.Condition == 1);

            
            //var coaches = CCoachViewModel.CoachList(datas.ToList());
            return View();
        }

        public IActionResult ViewCoachDetails(int? id)
        {
            //Coach coach = _context.Coaches.FirstOrDefault(c => c.CoachId == id);
            //if (coach == null)
            //    return RedirectToAction("CoachList");

            //var data = _context.Coaches.Where(c => c.Condition == 1 && c.CoachId == id)
            //    //.Include(c => c.Member).ThenInclude(cm => cm.ResidentialCity)
            //    //.Include(c => c.Type)
            //    //.Include(c => c.Courses)
            //    //.Include(c => c.Image)
            //    //.Include(c => c.Intro).AsEnumerable().FirstOrDefault();

            //CCoachViewModel vModel = new CCoachViewModel
            //{
            //    Coach = data
            //};
            return View();
        }

    }
}
