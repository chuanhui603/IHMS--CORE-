using Microsoft.AspNetCore.Mvc;
using IHMS.Models;
using IHMS.ViewModel;
using prjiHealth.ViewModel;
using HealthyLifeApp;
using System.Text.Json;


namespace IHMS.Controllers
{
    public class LoginController : Controller
    {
        private IWebHostEnvironment _enviro = null;
        public LoginController(IhmsContext context, IWebHostEnvironment p)
        {
            _enviro = p;

        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult MemberEdit(CKeywordViewModel vm)
        {
            string keyword = vm.txtKeyword;
            IhmsContext db = new IhmsContext();
            IEnumerable<Member> datas = null;
            if (string.IsNullOrEmpty(keyword))
            {
                datas = from c in db.Members
                        select c;
            }
            else
                datas = db.Members.Where(p => p.Name.Contains(keyword) ||
                p.Phone.Contains(keyword) ||
                p.Email.Contains(keyword) ||
                p.Account.Contains(keyword));
            return View(datas);

        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("MemberEdit");
            IhmsContext db = new IhmsContext();
            Member cust = db.Members.FirstOrDefault(p => p.MemberId == id);
            return View(cust);
        }
        [HttpPost]
        public ActionResult Edit(CMember x)
        {
            IhmsContext db = new IhmsContext();
            Member cust = db.Members.FirstOrDefault(p => p.MemberId == x.MemberId);
            if (cust != null)
            {
                if (x.photo != null)
                {
                    string photoName = Guid.NewGuid().ToString() + ".jpg";
                    x.photo.CopyTo(new FileStream(
                        _enviro.WebRootPath + "/images/" + photoName,
                        FileMode.Create));
                    cust.AvatarImage = photoName;
                }
                cust.Name = x.Name; //姓名
                cust.Phone = x.Phone; //電話
                cust.Email = x.Email; //信箱
                cust.Account = x.Account; //帳號
                cust.Password = x.Password; //密碼
                cust.Birthday = x.Birthday; //生日                               
                //cust.AvatarImage = x.AvatarImage; // 頭像
                cust.ResidentialCity = x.ResidentialCity; //居住城市              
                cust.DiseaseDescription = x.DiseaseDescription; //疾病史
                cust.AllergyDescription = x.AllergyDescription; //過敏反應
               
                db.SaveChanges();
            }
            return RedirectToAction("MemberEdit");
        }
    }
}
