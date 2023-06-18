using IHMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace IHMS.Controllers
{
    public class TeacherController : Controller
    {
        private IWebHostEnvironment _enviro = null;
        public TeacherController(IWebHostEnvironment e)
        {
            _enviro = e;
        }
        // GET: TeacherController
        public ActionResult List_Done()
        {
            IhmsContext db = new IhmsContext();
            var datas = from t in db.Teachers
                        where t.TCondition== 1
                        select t;
            //ViewBag["Cons"] = $"{顯示審核狀態(1)}";
            return View(datas);
        }
        public ActionResult List_UnReviewed()
        {
            IhmsContext db = new IhmsContext();
            var datas = from t in db.Teachers
                        where t.TCondition == 0 || t.TCondition == null
                        select t;
            return View(datas);
        }
        public ActionResult List_Rejected()
        {
            IhmsContext db = new IhmsContext();
            var datas = from t in db.Teachers
                        where t.TCondition == 2
                        select t;
            return View(datas);
        }
        public ActionResult List_resigned()
        {
            IhmsContext db = new IhmsContext();
            var datas = from t in db.Teachers
                        where t.TCondition == 3
                        select t;
            return View(datas);
        }

        // GET: TeacherController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TeacherController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TeacherController/Create
        [HttpPost]
       
        public ActionResult Create(Teacher t)
        {
            IhmsContext db = new IhmsContext();
            db.Teachers.Add(t);
            db.SaveChanges();
            return RedirectToAction("List_Done");
        }

        // GET: TeacherController/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
                return RedirectToAction("List_Done");
            IhmsContext db = new IhmsContext();
            Teacher coach = db.Teachers.FirstOrDefault(c => c.TTeacherId == id);
            return View(coach);
        }

        // POST: TeacherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CTeacherWrapper x)
        {
            IhmsContext db = new IhmsContext();
            Teacher coach = db.Teachers.FirstOrDefault(c => c.TTeacherId == x.TeacherId);

            if (coach != null)
            {
                if (x.photo != null)
                {
                    string photoName = Guid.NewGuid().ToString() + ".jpg";
                    x.photo.CopyTo(new FileStream(
                        _enviro.WebRootPath + "/images/" + photoName,
                        FileMode.Create));
                    coach.TImage = photoName;
                }
                coach.TIntro = x.Intro;
                coach.TVideo = x.Video;
                coach.TResume = x.Resume;
                coach.TApplytime = DateTime.Now;

                db.SaveChanges();
            }
            return RedirectToAction("List_Done");
        }

        // GET: TeacherController/Delete/5
        public ActionResult Delete(int id)
        {
            if (id != null)
            {
                IhmsContext db = new IhmsContext();
                Teacher coach = db.Teachers.FirstOrDefault(p => p.TTeacherId == id);
                if (coach != null)
                {
                    db.Teachers.Remove(coach);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("List_Done");
        }
        /*public string 顯示審核狀態(int Con)
        {
            string strCon = "";

            Dictionary<int, string> dict審核狀態_zh_TW = new Dictionary<int, string>();
            dict審核狀態_zh_TW.Add((int)CMyEnums.Condition.未審核, "未審核");
            dict審核狀態_zh_TW.Add((int)CMyEnums.Condition.已審核, "已審核");
            dict審核狀態_zh_TW.Add((int)CMyEnums.Condition.退件, "退件");
            dict審核狀態_zh_TW.Add((int)CMyEnums.Condition.離職, "離職");

            Dictionary<int, string> dict選用名稱 = new Dictionary<int, string>();
            dict選用名稱 = dict審核狀態_zh_TW;
            strCon = dict選用名稱[Con];

            return strCon;
        }*/


    }
}
