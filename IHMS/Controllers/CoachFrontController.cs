using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IHMS.Models;
using IHMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections;

namespace IHMS.Controllers
{
    public class CoachFrontController : Controller
    {
        private IhmsContext db;
        private readonly IhmsContext _context;
        

        private IWebHostEnvironment _environment;
        public CoachFrontController(IhmsContext context, IWebHostEnvironment environment)
        {
            db = context;
            _context = context;
            _environment = environment;
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
            return View();
        }

        //教練詳細資料
        public IActionResult ViewCoachDetails(int? id)
        {
            IEnumerable<Coach> datas = null;
            Coach coach = _context.Coaches.FirstOrDefault(c => c.CoachId == id);
            if (coach == null)
                return RedirectToAction("CoachList");

            var data = _context.Coaches.Where(c => c.CoachId == id)
                .Include(c => c.CoachSkills)
                .Include(c => c.CoachAvailableTimes)
                .Include(c => c.CoachExperiences)
                .Include(c => c.CoachLicenses)
                .FirstOrDefault(c => c.MemberId == id);

            CCoachViewModel vModel = new CCoachViewModel
            {
                Coach = (Coach)data
            };
            return View(vModel);
        }
        

        //教練預約時間表-可預約
        public IActionResult getAvailableTimeId(int? id)
        {
            var ids = db.CoachAvailableTimes.Where(ca => ca.CoachId == id).Select(ca => ca.AvailableTimeId).Distinct();
            return Json(ids);
        }
        //已額滿課程時間
        public IActionResult getAvailableTimeNum(int? id)
        {
            var nums = db.Courses.Where(c => c.CoachContact.CoachId == id && c.StatusNumber == 55).Select(c => c.AvailableTimeNum).Distinct();
            return Json(nums);
        }
        //通知教練
        public IActionResult createContact(Member Email)
        {
            //取得登入者ID
            
            return Content("");
        }


        //填寫履歷
        public IActionResult CreateResume()
        {
            int userId = 8; //備用帳號

            Coach c = _context.Coaches.FirstOrDefault(c => c.MemberId == userId);
            if (c != null)
                return RedirectToAction("EditResume");
            CCoachViewModel vModel = new CCoachViewModel
            {
                Coach = new Coach()
            };
            return View(vModel);
        }
        [HttpPost] //送出履歷
        public IActionResult CreateResume(Coach c, IFormFile File, int[] CoachSkill, int[] CoachTime, string[] Experience, string[] License)
        {
            int userId = 8;
           
           
            c.MemberId = userId;
            c.StatusNumber = 1;
            c.Visible = false;
            c.ApplyDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            _context.Coaches.Add(c);
            _context.SaveChanges();
            if (File != null)
            {
                string photoName = Guid.NewGuid().ToString() + ".jpg";
                File.CopyTo(new FileStream(_environment.WebRootPath + "/img/coach/coachImage/" + photoName, FileMode.Create));
                c.CoachImage = photoName;
            }

            //新增Skills            
            foreach (int skillId in CoachSkill)
            {
                CoachSkill newSkill = new CoachSkill
                {
                    CoachId = c.CoachId,
                    SkillId = skillId
                };
                _context.CoachSkills.Add(newSkill);
            }

            //新增AvailableTime            
            foreach (int timeId in CoachTime)
            {
                CoachAvailableTime newTime = new CoachAvailableTime
                {
                    CoachId = c.CoachId,
                    AvailableTimeId = timeId
                };
                _context.CoachAvailableTimes.Add(newTime);
            }

            //新增Experience            
            foreach (string Exp in Experience)
            {
                if (Exp != null)
                {
                    CoachExperience newExp = new CoachExperience
                    {
                        CoachId = c.CoachId,
                        Experience = Exp.Trim()
                    };
                    _context.CoachExperiences.Add(newExp);
                }
            }

            //新增License           
            foreach (string Lic in License)
            {
                if (Lic != null)
                {
                    CoachLicense newLic = new CoachLicense
                    {
                        CoachId = c.CoachId,
                        License = Lic.Trim()
                    };
                    _context.CoachLicenses.Add(newLic);
                }
            }
            _context.SaveChanges();
            return Content("Success", "text/plain");
        }
        //修改履歷
        public IActionResult EditResume()
        {
            int userId = 11;

            Coach data = _context.Coaches
                .Include(c => c.CoachSkills)
                .Include(c => c.CoachAvailableTimes)
                .Include(c => c.CoachExperiences)
                .Include(c => c.CoachLicenses)
                .FirstOrDefault(c => c.MemberId == userId);
            CCoachViewModel vModel = new CCoachViewModel
            {
                Coach = data
            };
            return View(vModel);
        }

        [HttpPost]  //送出修改
        public IActionResult EditResume(Coach c, IFormFile File, int[] CoachSkill, int[] CoachTime, string[] Experience, string[] License)
        {
            int userId = 11;
            
            Coach coach = _context.Coaches.FirstOrDefault(c => c.MemberId == userId);
            if (coach != null)
            {
                if (File != null)
                {
                    string photoName = Guid.NewGuid().ToString() + ".jpg";
                    File.CopyTo(new FileStream(_environment.WebRootPath + "/img/coach/coachImage/" + photoName, FileMode.Create));
                    coach.CoachImage = photoName;
                }
                coach.CityId = c.CityId;
                coach.CoachFee = c.CoachFee;
                coach.CoachDescription = c.CoachDescription;
                coach.Slogan = c.Slogan;

                //新增Skills
                var currentSkills = _context.CoachSkills.Where(cs => cs.CoachId == coach.CoachId).ToList();
                foreach (var skill in currentSkills)
                    _context.CoachSkills.Remove(skill);
                foreach (int skillId in CoachSkill)
                {
                    CoachSkill newSkill = new CoachSkill
                    {
                        CoachId = coach.CoachId,
                        SkillId = skillId
                    };
                    _context.CoachSkills.Add(newSkill);
                }

                //新增AvailableTime
                var currentTime = _context.CoachAvailableTimes.Where(at => at.CoachId == coach.CoachId).ToList();
                foreach (var time in currentTime)
                    _context.CoachAvailableTimes.Remove(time);
                foreach (int timeId in CoachTime)
                {
                    CoachAvailableTime newTime = new CoachAvailableTime
                    {
                        CoachId = coach.CoachId,
                        AvailableTimeId = timeId
                    };
                    _context.CoachAvailableTimes.Add(newTime);
                }

                //新增Experience
                var currentExp = _context.CoachExperiences.Where(e => e.CoachId == coach.CoachId).ToList();
                foreach (var exp in currentExp)
                    _context.CoachExperiences.Remove(exp);
                foreach (string Exp in Experience)
                {
                    if (Exp != null)
                    {
                        CoachExperience newExp = new CoachExperience
                        {
                            CoachId = coach.CoachId,
                            Experience = Exp.Trim()
                        };
                        _context.CoachExperiences.Add(newExp);
                    }
                }

                //新增License
                var currentLic = _context.CoachLicenses.Where(e => e.CoachId == coach.CoachId).ToList();
                foreach (var lic in currentLic)
                    _context.CoachLicenses.Remove(lic);
                foreach (string Lic in License)
                {
                    if (Lic != null)
                    {
                        CoachLicense newLic = new CoachLicense
                        {
                            CoachId = coach.CoachId,
                            License = Lic.Trim()
                        };
                        _context.CoachLicenses.Add(newLic);
                    }
                }
            }
            _context.SaveChanges();
            return Content("Success", "text/plain");
        }
        //教課列表
        public IActionResult TeachingList()
        {
            int userId = 11;
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_Logined_User))
            {
                string json = HttpContext.Session.GetString(CDictionary.SK_Logined_User);
                userId = (JsonSerializer.Deserialize<Member>(json)).MemberId;
            }
            var coach = _context.Coaches.Where(c => c.StatusNumber == 2).FirstOrDefault(c => c.MemberId == userId);
            if (coach == null)
                return RedirectToAction("CreateResume");

            var data = _context.Courses
                .Include(c => c.CoachContact).ThenInclude(cc => cc.MemberId)
                .Include(c => c.Schedules)
                .Where(c => c.CoachContact.CoachId == coach.CoachId).ToList();
            return View(CTeachingListViewModel.CourseList(data));
        }

        //完成排課
        public IActionResult ReservationDone(int id)
        {
            var reservation = _context.Schedules.FirstOrDefault(r => r.ScheduleId == id);
            reservation.StatusNumber = 61;
            _context.SaveChanges();

            //若Reservation皆結束，即修改課程狀態為「已結束」
            int courseId = (int)reservation.CourseId;
            if (_context.Schedules.Where(r => r.CourseId == courseId).Select(r => r.StatusNumber).ToList().All(num => num == 61))
            {
                var thisCourse = _context.Courses.FirstOrDefault(c => c.CourseId == courseId);
                thisCourse.StatusNumber = 56;
            }
            _context.SaveChanges();
            return Content("Success", "text/plain");
        }
        //更改時間
        public IActionResult EditReservation(int id, string date, string time)
        {
            //取得該教練排課
            int userId = 11;
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_Logined_User))
            {
                string json = HttpContext.Session.GetString(CDictionary.SK_Logined_User);
                userId = (JsonSerializer.Deserialize<Member>(json)).MemberId;
            }
            var coachId = _context.Coaches.FirstOrDefault(c => c.MemberId == userId).CoachId;
            var reservations = _context.Schedules.Include(r => r.Course).ThenInclude(c => c.CoachContact)
                .Where(r => r.Course.CoachContact.CoachId == coachId);

            //比對教練該時段是否已額滿
            var occupied = reservations.Where(r => r.CourseTime.Substring(0, 8) == date.Replace("-", ""))
                .Select(r => Convert.ToInt32(r.CourseTime.Substring(8, 2))).ToList();
            if (occupied.Contains(Convert.ToInt32(time)))
                return Content("Fail", "text/plain");
            else
            {
                var reservation = _context.Schedules.FirstOrDefault(r => r.ScheduleId == id);
                string newDate = date.Replace("-", "");
                string newTime = time.Length == 1 ? "0" + time : time;
                reservation.CourseTime = newDate + newTime + "00";
                _context.SaveChanges();
                return Content("Success", "text/plain");
            }
        }
        //取得進行中課程
        public IActionResult GetCourseInProcess(int id)
        {
            var courses = _context.Courses.Where(c => c.StatusNumber == id).Select(c => c.CourseId).ToList();
            return Json(courses);
        }
        //取得已結束課程
        public IActionResult GetCourseDone(int id)
        {
            var courses = _context.Courses.Where(c => c.StatusNumber == id).Select(c => c.CourseId).ToList();
            return Json(courses);
        }
    }
}
