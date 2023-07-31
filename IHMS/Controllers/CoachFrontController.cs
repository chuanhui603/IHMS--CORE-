﻿using Microsoft.AspNetCore.Http;
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
        public CoachFrontController(IhmsContext context)
        {
            db = context;
            _context = context;
        }

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
            var ids = db.Schedules.Where(ca => ca.CourseId == id).Select(ca => ca.CourseTime).Distinct();
            return Json(ids);
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
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_Logined_User))
            {
                string json = HttpContext.Session.GetString(CDictionary.SK_Logined_User);
                userId = (JsonSerializer.Deserialize<Member>(json)).MemberId;
            }
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
        public IActionResult CreateResume(Coach c)
        {
            int userId = 8;
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_Logined_User))
            {
                string json = HttpContext.Session.GetString(CDictionary.SK_Logined_User);
                userId = (JsonSerializer.Deserialize<Member>(json)).MemberId;
            }
            c.MemberId = userId;
            c.StatusNumber = 0;
            c.ApplyDate = DateTime.Now.ToString();
            _context.Coaches.Add(c);
            _context.SaveChanges();
            //if (File != null)
            //{
            //    string photoName = Guid.NewGuid().ToString() + ".jpg";
            //    File.CopyTo(new FileStream(_environment.WebRootPath + "/img/coach/coachImage/" + photoName, FileMode.Create));
            //    c.Image = photoName;
            //}
            //新增Skills            
            //foreach (int skill in )
            //{
            //    Coach newSkill = new Coach
            //    {
            //        CoachId = c.CoachId,
            //        Type= c.
            //    };
            //    _context.TCoachSkills.Add(newSkill);
            //}

            //新增AvailableTime            
            //foreach (int timeId in Schedule)
            //{
            //    Schedule newTime = new Schedule();
            //    {
            //        FCoachId = c.CoachId,
            //        FAvailableTimeId = timeId
            //    };
            //    _context.TCoachAvailableTimes.Add(newTime);
            //}

            //新增Experience            
            //foreach (string Exp in fExperience)
            //{
            //    if (Exp != null)
            //    {
            //        TCoachExperience newExp = new TCoachExperience
            //        {
            //            FCoachId = c.FCoachId,
            //            FExperience = Exp.Trim()
            //        };
            //        _context.TCoachExperiences.Add(newExp);
            //    }
            //}

            //新增License           
            //foreach (string Lic in fLicense)
            //{
            //    if (Lic != null)
            //    {
            //        TCoachLicense newLic = new TCoachLicense
            //        {
            //            FCoachId = c.FCoachId,
            //            FLicense = Lic.Trim()
            //        };
            //        _context.TCoachLicenses.Add(newLic);
            //    }
            //}
            _context.SaveChanges();
            return Content("Success", "text/plain");
        }
        //修改履歷
        public IActionResult EditResume()
        {
            int userId = 11;
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_Logined_User))
            {
                string json = HttpContext.Session.GetString(CDictionary.SK_Logined_User);
                userId = (JsonSerializer.Deserialize<Member>(json)).MemberId;
            }
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
        public IActionResult EditResume(Coach c)
        {
            int userId = 11;
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_Logined_User))
            {
                string json = HttpContext.Session.GetString(CDictionary.SK_Logined_User);
                userId = (JsonSerializer.Deserialize<Member>(json)).MemberId;
            }
            Coach coach = _context.Coaches.FirstOrDefault(c => c.MemberId == userId);
            //if (coach != null)
            //{
            //    if (File != null)
            //    {
            //        string photoName = Guid.NewGuid().ToString() + ".jpg";
            //        File.CopyTo(new FileStream(_environment.WebRootPath + "/img/coach/coachImage/" + photoName, FileMode.Create));
            //        coach.FCoachImage = photoName;
            //    }
            //    coach.FCityId = c.FCityId;
            //    coach.FCoachFee = c.FCoachFee;
            //    coach.FCoachDescription = c.FCoachDescription;
            //    coach.FSlogan = c.FSlogan;

            //    //新增Skills
            //    var currentSkills = _context.TCoachSkills.Where(cs => cs.FCoachId == coach.FCoachId).ToList();
            //    foreach (var skill in currentSkills)
            //        _context.TCoachSkills.Remove(skill);
            //    foreach (int skillId in fCoachSkill)
            //    {
            //        TCoachSkill newSkill = new TCoachSkill
            //        {
            //            FCoachId = coach.FCoachId,
            //            FSkillId = skillId
            //        };
            //        _context.TCoachSkills.Add(newSkill);
            //    }

            //    //新增AvailableTime
            //    var currentTime = _context.TCoachAvailableTimes.Where(at => at.FCoachId == coach.FCoachId).ToList();
            //    foreach (var time in currentTime)
            //        _context.TCoachAvailableTimes.Remove(time);
            //    foreach (int timeId in fCoachTime)
            //    {
            //        TCoachAvailableTime newTime = new TCoachAvailableTime
            //        {
            //            FCoachId = coach.FCoachId,
            //            FAvailableTimeId = timeId
            //        };
            //        _context.TCoachAvailableTimes.Add(newTime);
            //    }

            //    //新增Experience
            //    var currentExp = _context.TCoachExperiences.Where(e => e.FCoachId == coach.FCoachId).ToList();
            //    foreach (var exp in currentExp)
            //        _context.TCoachExperiences.Remove(exp);
            //    foreach (string Exp in fExperience)
            //    {
            //        if (Exp != null)
            //        {
            //            TCoachExperience newExp = new TCoachExperience
            //            {
            //                FCoachId = coach.FCoachId,
            //                FExperience = Exp.Trim()
            //            };
            //            _context.TCoachExperiences.Add(newExp);
            //        }
            //    }

            //    //新增License
            //    var currentLic = _context.TCoachLicenses.Where(e => e.FCoachId == coach.FCoachId).ToList();
            //    foreach (var lic in currentLic)
            //        _context.TCoachLicenses.Remove(lic);
            //    foreach (string Lic in fLicense)
            //    {
            //        if (Lic != null)
            //        {
            //            TCoachLicense newLic = new TCoachLicense
            //            {
            //                FCoachId = coach.FCoachId,
            //                FLicense = Lic.Trim()
            //            };
            //            _context.TCoachLicenses.Add(newLic);
            //        }
            //    }
            //}
            _context.SaveChanges();
            return Content("Success", "text/plain");
        }

    }
}