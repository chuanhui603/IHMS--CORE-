using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IHMS.Models;
using IHMS.DTO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using IHMS.ViewModel;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;


namespace IHMS.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IhmsContext _context;
        private readonly IConfiguration _configuration;

        public LoginController(IhmsContext context, IConfiguration configuration) // 加入這行
        {
            _context = context;
            _configuration = configuration; // 加入這行
        }

        // GET: api/Login
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
          if (_context.Members == null)
          {
              return NotFound();
          }
            return await _context.Members.ToListAsync();
        }

        // GET: api/Login/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(int id)
        {
          if (_context.Members == null)
          {
              return NotFound();
          }
            var member = await _context.Members.FindAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return member;
        }

        // PUT: api/Login/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMember(int id, Member member)
        {
            if (id != member.MemberId)
            {
                return BadRequest();
            }

            _context.Entry(member).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Login
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Member>> PostMember(Member member)
        {
          if (_context.Members == null)
          {
              return Problem("Entity set 'IhmsContext.Members'  is null.");
          }
            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMember", new { id = member.MemberId }, member);
        }

        // DELETE: api/Login/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            if (_context.Members == null)
            {
                return NotFound();
            }
            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // POST: api/Members/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] MembersDTO loginDTO)
        {
            string username = loginDTO.Account; // 用戶輸入的帳號
            string password = loginDTO.Password; // 用戶輸入的密碼

            // 根據帳號查找對應的會員記錄
            var member = await _context.Members.SingleOrDefaultAsync(m => m.Account == username);

            if (member == null)
            {
                // 如果找不到對應的會員記錄，表示帳號錯誤
                return BadRequest(new { message = "帳號錯誤" });
            }

            // 比對用戶提供的密碼和資料庫中的密碼
            bool isPasswordCorrect = string.Compare(password, member.Password, false) == 0;
            if (isPasswordCorrect)
            {
                
                // 登入成功
                return Ok(new { message = "登入成功"});
            }
            else
            {
                // 密碼驗證失敗
                return BadRequest(new { message = "登入失敗，帳號或密碼錯誤" });
            }
        }

        private bool MemberExists(int id)
        {
            return (_context.Members?.Any(e => e.MemberId == id)).GetValueOrDefault();
        }


        // 在 API 中處理 Google 登入回調
        [AllowAnonymous]
        [HttpPost("GoogleLoginCallback")]
        public async Task<IActionResult> GoogleLoginCallback([FromBody] GoogleLoginCallbackViewModel model)
        {
            // 取得 Google 的使用者信箱
            var googleEmail = model.Email;

            // 在本地資料庫中查詢該使用者是否已註冊過
            var existingUser = await _context.Members.FirstOrDefaultAsync(m => m.Email == googleEmail);

            if (existingUser != null)
            {
                // 如果已註冊，則進行登入
                var token = GenerateToken(existingUser); // 產生 JWT Token，用於後續的驗證
                return Ok(new { token });
            }
            else
            {
                // 如果未註冊，則根據 Google 的使用者資訊新增一個新的帳號並進行登入
                var newUser = new Member
                {
                    Email = googleEmail,
                    // 其他資訊可以根據需要從 Google 資訊中取得
                    // 例如：姓名、生日、性別等等，可以透過 Google 資料填充到這裡
                };

                _context.Members.Add(newUser);
                await _context.SaveChangesAsync();

                // 導向 Members/SignIn 頁面並將 Google 資料傳遞過去
                return RedirectToAction("SignIn", "Members", new
                {
                    email = newUser.Email,
                    givenName = newUser.Name,
                    familyName = newUser.Name,
                    birthDate = newUser.Birthday,
                    gender = newUser.Gender,
                    // 其他需要傳遞的資料
                });
            }

        }

        // 產生 JWT Token 的方法
        private string GenerateToken(Member user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                // 其他需要加入的 Claim
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
