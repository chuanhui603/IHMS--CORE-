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
        private readonly IhmsContext _context;
        private IWebHostEnvironment _enviro = null;
        private IWebHostEnvironment _environment;
        public LoginController(IhmsContext context, IWebHostEnvironment p, IWebHostEnvironment iwhe)
        {
            _enviro = p;
            _context = context;
            _environment = iwhe;
        }
        public IActionResult Login()
        {
            return View();
        }        
        [HttpPost]
        public IActionResult Login(CLoginViewModel vModel)
        {
            if (vModel.fAccount == null || vModel.fPassword == null)
            {
                return Content("empty", "text/plain", System.Text.Encoding.UTF8);
            }

            var q = _context.Members.FirstOrDefault(tm => tm.Account == vModel.fAccount);
            if (q != null)
            {
                if (q.Password == utilities.getCryptPWD(vModel.fPassword, vModel.fAccount))
                {
                    string loginSession = JsonSerializer.Serialize(q);
                    HttpContext.Session.SetString(CDictionary.SK_Logined_User, loginSession);
                    Member loginUser = JsonSerializer.Deserialize<Member>(loginSession);
                    int authorId = (int)loginUser.Permission;
                    if (authorId < 5)
                    {
                        string admin = "admin" + loginUser.Name;
                        return Content(admin, "text/plain", System.Text.Encoding.UTF8);
                    }
                    else
                    {
                        return Content(loginUser.Name, "text/plain", System.Text.Encoding.UTF8);
                    }
                }
            }
            return Content("false", "text/plain", System.Text.Encoding.UTF8);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove(CDictionary.SK_Logined_User);
            HttpContext.Session.Remove(CDictionary.SK_Shopped_Items);
            HttpContext.Session.Remove(CDictionary.SK_Third_Party_Payment);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Edit()
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_Logined_User))
            {
                var memberEdit = HttpContext.Session.GetString(CDictionary.SK_Logined_User);
                Member loginUser = JsonSerializer.Deserialize<Member>(memberEdit);
                var q = _context.Members.FirstOrDefault(m => m.MemberId == loginUser.MemberId);
                return View(q);
            }
            else
            {
                var q = _context.Members.FirstOrDefault(m => m.MemberId == 8);
                return View(q);
            }
        }
        [HttpPost]
        public IActionResult Edit(CLoginViewModel vModel)
        {
            var q = _context.Members.FirstOrDefault(m => m.MemberId == vModel.fMemberId);
            if (q != null)
            {
                if (vModel.photo != null)
                {
                    string pName = Guid.NewGuid().ToString() + ".jpg";
                    vModel.photo.CopyTo(new FileStream(_environment.WebRootPath + "/img/member/" + pName, FileMode.Create));
                    q.AvatarImage = pName;
                }
                q.Account = vModel.fAccount;
                q.Birthday = vModel.fBirthday;
                q.ResidentialCity = vModel.fResidentialCity;
                q.Phone = vModel.fPhone;
                q.Email = vModel.fEmail;
                q.Phone = vModel.fPhone;
                _context.SaveChanges();
                return RedirectToAction("Edit", "Member");
            }
            else { return RedirectToAction("Edit", "Member"); }
        }
    }
}
