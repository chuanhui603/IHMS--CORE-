using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IHMS.Models;
using IHMS.DTO;

namespace IHMS.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IhmsContext _context;

        public MembersController(IhmsContext context)
        {
            _context = context;
        }

        // GET: api/Members
        [Route("api/[controller]/{memberid:int}/{search}")]
        [HttpGet]
        public async Task<IEnumerable<MembersDTO>> GetMembers(int id, string search)
        {
            return _context.Members.Select(
                mem => new MembersDTO
                {
                    MemberId = mem.MemberId,
                    Name = mem.Name,
                    Email = mem.Email,
                    Phone = mem.Phone,
                    Account = mem.Account,
                    Password = mem.Password,
                    Birthday = mem.Birthday,
                    Gender = mem.Gender,
                    MaritalStatus = mem.MaritalStatus,
                    Nickname = mem.Nickname,
                    AvatarImage = mem.AvatarImage,
                    ResidentialCity = mem.ResidentialCity,
                    Permission = mem.Permission,
                    Occupation = mem.Occupation,
                    DiseaseDescription = mem.DiseaseDescription,
                    AllergyDescription = mem.AllergyDescription,
                    LoginTime = mem.LoginTime
                });
        }

        // GET: api/Members/5
        [HttpGet("{id}")]
        public async Task<MembersDTO> GetMember(int id)
        {
            if (_context.Members == null)
            {
                return null;
            }
            var member = _context.Members.Where(mem => mem.MemberId == id).Select(mem => new MembersDTO
            {
                MemberId = mem.MemberId,
                Name = mem.Name,
                Email = mem.Email,
                Phone = mem.Phone,
                Account = mem.Account,
                Password = mem.Password,
                Birthday = mem.Birthday,
                Gender = mem.Gender,
                MaritalStatus = mem.MaritalStatus,
                Nickname = mem.Nickname,
                AvatarImage = mem.AvatarImage,
                ResidentialCity = mem.ResidentialCity,
                Permission = mem.Permission,
                Occupation = mem.Occupation,
                DiseaseDescription = mem.DiseaseDescription,
                AllergyDescription = mem.AllergyDescription,
                LoginTime = mem.LoginTime
            }).SingleOrDefault();

            if (member == null)
            {
                return null;
            }

            return member;
        }

        // PUT: api/Members/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<string> PutMember(int id, MembersDTO MemDTO)//修改會員資料
        {
            {
                if (id != MemDTO.MemberId)
                {
                    return "修改會員紀錄失敗!";
                }
                Member mem = await _context.Members.FindAsync(id);
                if (mem != null)
                {
                    mem.Name = MemDTO.Name;
                    mem.Email = MemDTO.Email;
                    mem.Phone = MemDTO.Phone;
                    mem.Account = MemDTO.Account;
                    mem.Password = MemDTO.Password;
                    mem.Birthday = MemDTO.Birthday;
                    mem.Gender = MemDTO.Gender;
                    mem.MaritalStatus = MemDTO.MaritalStatus;
                    mem.Nickname = MemDTO.Nickname;
                    mem.AvatarImage = MemDTO.AvatarImage;
                    mem.ResidentialCity = MemDTO.ResidentialCity;
                    mem.Permission = MemDTO.Permission;
                    mem.Occupation = MemDTO.Occupation;
                    mem.DiseaseDescription = MemDTO.DiseaseDescription;
                    mem.AllergyDescription = MemDTO.AllergyDescription;
                    mem.LoginTime = MemDTO.LoginTime;
                    _context.Entry(mem).State = EntityState.Modified;
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!MemberExists(id))
                        {
                            return "修改會員紀錄失敗";
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                else
                {
                    return "修改會員紀錄失敗";
                }
                return "會員紀錄修改成功";

            }
        }

        // POST: api/Members
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<string> PostMember(MembersDTO MemDTO)//註冊會員
        {
            Member mem = new Member
            {
                Name = MemDTO.Name,
                Email = MemDTO.Email,
                Account = MemDTO.Account,
                Password = MemDTO.Password
            };
            _context.Members.Add(mem);
            await _context.SaveChangesAsync();

            return "註冊成功";
        }

        // DELETE: api/Members/5
        [HttpDelete("{id}")]
        public async Task<string> DeleteMember(int id)//刪除
        {
            if (_context.Members == null)
            {
                return "刪除失敗!";
            }
            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return "刪除失敗!";
            }
            try
            {
                _context.Members.Remove(member);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return "刪除失敗!";
            }

            return "刪除成功!";
        }

        private bool MemberExists(int id)
        {
            return (_context.Members?.Any(e => e.MemberId == id)).GetValueOrDefault();
        }
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

            // 檢查用戶提供的密碼是否與資料庫中的密碼匹配
            if (password == member.Password)
            {
                // 登入成功
                return Ok(new { message = "登入成功" });
            }
            else
            {
                // 密碼驗證失敗
                return BadRequest(new { message = "登入失敗，帳號或密碼錯誤" });
            }
        }     
    }
}
