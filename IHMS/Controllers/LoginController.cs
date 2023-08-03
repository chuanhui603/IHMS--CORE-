using Microsoft.AspNetCore.Mvc;
using IHMS.Models;
using IHMS.ViewModel;
using prjiHealth.ViewModel;
using HealthyLifeApp;
using System.Text.Json;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;

namespace IHMS.Controllers
{
    public class LoginController : Controller
    {
        private IWebHostEnvironment _enviro = null;
        private readonly IhmsContext _context;

        public LoginController(IhmsContext context, IWebHostEnvironment p)
        {
            _context = context;
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
            return RedirectToAction("Edit");
        }
        /// <summary>
        /// 驗證 Google 登入授權
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GoogleLogin()
        {
            string? formCredential = Request.Form["credential"];
            string? formToken = Request.Form["g_csrf_token"];
            string? cookiesToken = Request.Cookies["g_csrf_token"];

            GoogleJsonWebSignature.Payload? payload = await VerifyGoogleToken(formCredential, formToken, cookiesToken);

            if (payload == null)
            {
                ViewData["Msg"] = "验证 Google 授权失败";
            }
            else
            {
                var member = await _context.Members.FirstOrDefaultAsync(m => m.Email == payload.Email);

                if (member != null)
                {
                    ViewData["MemberInfo"] = member;
                    return View("MemberDashboard"); // 如果有註冊
                }
                else
                {
                    //用ViewModel試看看
                    ViewData["GoogleEmail"] = payload.Email;
                    Console.WriteLine(payload.Email);
                    return RedirectToAction("SignIn", "Members");// 如果沒有註冊，則進入註冊頁面
                }
            }

            return View();
        }



        /// <summary>
        /// 驗證 Google Token
        /// </summary>
        /// <param name="formCredential"></param>
        /// <param name="formToken"></param>
        /// <param name="cookiesToken"></param>
        /// <returns></returns>
        public async Task<GoogleJsonWebSignature.Payload?> VerifyGoogleToken(string? formCredential, string? formToken, string? cookiesToken)
        {
            // 檢查空值
            if (formCredential == null || formToken == null && cookiesToken == null)
            {
                return null;
            }

            GoogleJsonWebSignature.Payload? payload;
            try
            {
                // 驗證 token
                if (formToken != cookiesToken)
                {
                    return null;
                }

                // 驗證憑證
                IConfiguration Config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
                string GoogleApiClientId = Config.GetSection("GoogleApiClientId").Value;
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { GoogleApiClientId }
                };
                payload = await GoogleJsonWebSignature.ValidateAsync(formCredential, settings);
                if (!payload.Issuer.Equals("accounts.google.com") && !payload.Issuer.Equals("https://accounts.google.com"))
                {
                    return null;
                }
                if (payload.ExpirationTimeSeconds == null)
                {
                    return null;
                }
                else
                {
                    DateTime now = DateTime.Now.ToUniversalTime();
                    DateTime expiration = DateTimeOffset.FromUnixTimeSeconds((long)payload.ExpirationTimeSeconds).DateTime;
                    if (now > expiration)
                    {
                        return null;
                    }
                }
            }
            catch
            {
                return null;
            }
            return payload;
        }

    }    
}
