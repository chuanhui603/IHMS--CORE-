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
using Microsoft.AspNetCore.Mvc.RazorPages;

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
            IEnumerable<Coach> datas = null;
            if (!String.IsNullOrEmpty(v.txtKeyword))
            {
                string keyword = v.txtKeyword.ToLower();
                datas = _context.Coaches
                    .Include(c => c.Member)
                    .Include(c => c.City)
                    .Include(c => c.CoachSkills).ThenInclude(cs => cs.Skill)
                    .Include(c => c.CoachExperiences)
                    .Include(c => c.CoachLicenses)
                    .Include(c => c.CoachRates).AsEnumerable()
                    .Where(c => c.Visible == true &&
                    (c.CoachName.ToLower().Contains(keyword) || c.City.CityName.ToLower().Contains(keyword) ||
                    c.CoachSkills.Any(cs => cs.Skill.SkillName.ToLower().Contains(keyword)) ||
                    c.CoachDescription.ToLower().Contains(keyword) || c.Slogan.ToLower().Contains(keyword) ||
                    c.CoachExperiences.Any(ce => ce.Experience.ToLower().Contains(keyword)) || c.CoachLicenses.Any(ce => ce.License.ToLower().Contains(keyword))));
            }
            else
            {
                datas = _context.Coaches
                                    .Include(c => c.Member)
                                    .Include(c => c.City)
                                    .Include(c => c.CoachSkills).ThenInclude(cs => cs.Skill)
                                    .Include(c => c.CoachRates).AsEnumerable()
                                    .Where(c => c.Visible == true);
            }
            ViewBag.Keyword = v.txtKeyword;
            var coaches = CCoachViewModel.CoachList(datas.ToList());
            return View(coaches);
        }
        //Ajax多重篩選
        public IActionResult MultiFilter(int? CityId, string[] Gender, int[] CoachSkill, int[] CoachTime)
        {
            var datas = _context.Coaches
                .Include(c => c.Member)
                .Include(c => c.City)
                .Include(c => c.CoachSkills).ThenInclude(cs => cs.Skill)
                .Include(c => c.CoachAvailableTimes)
                .Include(c => c.CoachRates).AsEnumerable()
                .Where(c => c.Visible == true &&
                        (CityId != null ? c.CityId == CityId : true) &&
                        (Gender.Length != 0 ? Gender.Contains(c.Member.Gender.ToString()) : true) &&
                        (CoachSkill.Length != 0 ? c.CoachSkills.Select(cs => (int)cs.SkillId).ToArray<int>().Intersect<int>(CoachSkill).Count() > 0 : true) &&
                        (CoachTime.Length != 0 ? c.CoachAvailableTimes.Select(at => (int)at.AvailableTimeId).ToArray<int>().Intersect<int>(CoachTime).Count() > 0 : true));

            var coaches = CCoachViewModel.CoachList(datas.ToList());
            return Json(coaches);
        }
        public IActionResult ViewCoachDetails(int? id)
        {
            Coach coach = _context.Coaches.FirstOrDefault(c => c.CoachId == id);
            if (coach == null)
                return RedirectToAction("CoachList");

            var data = _context.Coaches.Where(c => c.Visible == true && c.CoachId == id)
                .Include(c => c.City)
                .Include(c => c.CoachSkills).ThenInclude(cs => cs.Skill)
                .Include(c => c.CoachAvailableTimes)
                .Include(c => c.CoachExperiences)
                .Include(c => c.CoachLicenses).AsEnumerable().FirstOrDefault();

            CCoachViewModel vModel = new CCoachViewModel
            {
                Coach = data
            };
            return View(vModel);
        }
        
        public IActionResult GetRecCoach(int id)
        {
            var cityId = _context.Coaches.FirstOrDefault(c => c.CoachId == id).CityId;
            var skills = _context.CoachSkills.Where(cs => cs.CoachId == id).Select(cs => cs.SkillId).ToArray();
            var datas = _context.Coaches
                .Include(c => c.Member)
                .Include(c => c.City)
                .Include(c => c.CoachSkills).ThenInclude(cs => cs.Skill).AsEnumerable()
                .Where(c => c.Visible == true && c.CityId == cityId && c.CoachId != id &&
                (c.CoachSkills.Select(cs => cs.SkillId).ToArray().Intersect(skills)).Count() > 0)
                .OrderBy(c => Guid.NewGuid()).Take(3);

            var coaches = CCoachViewModel.CoachList(datas.ToList());
            return Json(coaches);
        }
        public IActionResult getAvailableTimeId(int? id)
        {
            var ids = db.CoachAvailableTimes.Where(ca => ca.CoachId == id).Select(ca => ca.AvailableTimeId).Distinct();
            return Json(new { success = true, data = ids });
        }
        
        //教練預約時間表-已額滿
        public IActionResult getAvailableTimeNum(int? id)
        {

            var nums = db.Courses.Where(c => c.CoachContact.CoachId == 1 && c.StatusNumber == 55).Select(c => c.AvailableTimeNum).Distinct();
            return Json(nums);
        }
        //取得推薦教練專長
        public IActionResult GetSkillName(int id)
        {
            int userId = 1;
            var data = _context.CoachSkills.Where(cs => cs.CoachId == 1).Include(cs => cs.Skill).Select(cs => cs.Skill.SkillName).ToArray();
            return Json(data);
        }
        //取得所有有上課的CoachId
        public IActionResult GetReservationCoachId()
        {
            int userId = 1;
            
            var coachIdList = _context.Schedules
                .Include(r => r.Course).ThenInclude(c => c.CoachContact).ThenInclude(c => c.Coach)
                .Where(r => r.Course.CoachContact.MemberId == userId)
                .Select(r => r.Course.CoachContact.CoachId).Distinct().ToList();
            return Json(coachIdList);
        }
    }
}
