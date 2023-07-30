using Microsoft.AspNetCore.Mvc;
using IHMS.Models;
using IHMS.ViewModel;


namespace IHMS.Controllers
{
    public class MembersController : Controller
    {
        private IWebHostEnvironment _enviro = null;
        public MembersController(IWebHostEnvironment p)
        {
            _enviro = p;
        }

        public IActionResult List(CKeywordViewModel vm)
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
                return RedirectToAction("List");
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
                cust.Gender = x.Gender; //性別
                cust.MaritalStatus = x.MaritalStatus; //婚姻狀態
                cust.Name = x.Nickname; //暱稱
                //cust.AvatarImage = x.AvatarImage; // 頭像
                cust.ResidentialCity = x.ResidentialCity; //居住城市
                cust.Permission = x.Permission; //權限
                cust.Occupation = x.Occupation; //職業
                cust.DiseaseDescription = x.DiseaseDescription; //疾病史
                cust.AllergyDescription = x.AllergyDescription; //過敏反應
                cust.LoginTime = x.LoginTime; //登入日期
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                IhmsContext db = new IhmsContext();
                Member prod = db.Members.FirstOrDefault(p => p.MemberId == id);
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
            IhmsContext db = new IhmsContext();

            if (string.IsNullOrEmpty(t.AvatarImage))
            {
                t.AvatarImage = "1.jpg";
            }

            db.Members.Add(t);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        public ActionResult SignIn(int? id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(Member t)
        {
            IhmsContext db = new IhmsContext();               

            if (string.IsNullOrEmpty(t.AvatarImage))
            {
                t.AvatarImage = "1.jpg";
            }

            db.Members.Add(t);
            db.SaveChanges();           

            return Redirect("https://localhost:7127/");
        }
            public ActionResult MemberEdit(int? id)
            {
                return View();
            }
            [HttpPost]
            public ActionResult MemberEdit(CMember x)
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
                    cust.Gender = x.Gender; //性別
                    cust.MaritalStatus = x.MaritalStatus; //婚姻狀態
                    cust.Name = x.Nickname; //暱稱
                    //cust.AvatarImage = x.AvatarImage; // 頭像
                    cust.ResidentialCity = x.ResidentialCity; //居住城市
                    cust.Permission = x.Permission; //權限
                    cust.Occupation = x.Occupation; //職業
                    cust.DiseaseDescription = x.DiseaseDescription; //疾病史
                    cust.AllergyDescription = x.AllergyDescription; //過敏反應
                    cust.LoginTime = x.LoginTime; //登入日期
                    db.SaveChanges();
                }
                return View(x);
            }

    }
}
