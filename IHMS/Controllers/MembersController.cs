using Microsoft.AspNetCore.Mvc;
using IHMS.Models;


namespace IHMS.Controllers
{
    public class MembersController : Controller
    {
        private IWebHostEnvironment _enviro = null;
        public MembersController(IWebHostEnvironment p)
        {
            _enviro = p;
        }
        public IActionResult List()
        {
            IfmsContext db = new IfmsContext();
            var datas = from c in db.Members
                        select c;
            return View(datas);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            IfmsContext db = new IfmsContext();
            Member cust = db.Members.FirstOrDefault(p => p.MMemberId == id);
            return View(cust);
        }
        [HttpPost]
        public ActionResult Edit(CMember x)
        {
            IfmsContext db = new IfmsContext();
            Member cust = db.Members.FirstOrDefault(p => p.MMemberId == x.MMemberId);
            if (cust != null)
            {
                if (x.photo != null)
                {
                    string photoName = Guid.NewGuid().ToString() + ".jpg";
                    x.photo.CopyTo(new FileStream(
                        _enviro.WebRootPath + "/images/" + photoName,
                        FileMode.Create));
                    cust.MAvatarImage = photoName;
                }                
                cust.MName = x.MName; //姓名
                cust.MPhone = x.MPhone; //電話
                cust.MEmail = x.MEmail; //信箱
                cust.MAccount = x.MAccount; //帳號
                cust.MPassword = x.MPassword; //密碼
                cust.MBirthday = x.MBirthday; //生日
                cust.MGender = x.MGender; //性別
                cust.MMaritalStatus = x.MMaritalStatus; //婚姻狀態
                cust.MName = x.MName; //暱稱
                //cust.MAvatarImage = x.MAvatarImage; // 頭像
                cust.MResidentialCity = x.MResidentialCity; //居住城市
                cust.MPermission = x.MPermission; //權限
                cust.MOccupation = x.MOccupation; //職業
                cust.MDiseaseDescription = x.MDiseaseDescription; //疾病史
                cust.MAllergyDescription = x.MAllergyDescription; //過敏反應
                cust.MLoginTime = x.MLoginTime; //登入日期
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                IfmsContext db = new IfmsContext();
                Member prod = db.Members.FirstOrDefault(p => p.MMemberId == id);
                if (prod != null)
                {
                    db.Members.Remove(prod);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("List");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Member t)
        {
            IfmsContext db = new IfmsContext();

            if (string.IsNullOrEmpty(t.MAvatarImage))
            {
                t.MAvatarImage = "1.jpg";
            }

            db.Members.Add(t);
            db.SaveChanges();
            return RedirectToAction("List");
        }

    }
}
