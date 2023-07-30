using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IHMS.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IHMS.Controllers.APIcontrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersDTOController : ControllerBase
    {
        private readonly IhmsContext _context;

        public MembersDTOController(IhmsContext context)
        {
            _context = context;
        }

        // GET: api/MembersDTO
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
          if (_context.Members == null)
          {
              return NotFound();
          }
            return await _context.Members.ToListAsync();
        }

        // GET: api/MembersDTO/5
        [HttpGet("GetMemberByOrderid/{orderId}")]
        public async Task<List<Member>> GetMemberByOrderid(int orderId)
        {
            List<OrderDetail> orderDetails = await _context.OrderDetails.Where(od => od.OrderId == orderId).ToListAsync();
            // 使用 Include 來避免嵌套迴圈的查詢
            List<Member> memberList = await _context.OrderDetails
                    .Where(od => od.OrderId == orderId)
                    .SelectMany(od => _context.Schedules
                        .Where(schedule => schedule.ScheduleId == od.ScheduleId)
                        .SelectMany(schedule => _context.Courses
                            .Where(course => course.CourseId == schedule.CourseId)
                            .SelectMany(course => _context.Coaches
                                .Where(coach => coach.CoachId == course.CoachContactId)
                                .SelectMany(coach => _context.Members
                                .Where(member => member.MemberId == coach.MemberId)
                                )
                            )
                        )
                    )
                    .ToListAsync();

            //資料庫重複關聯問題
            //在序列化時，ReferenceHandler.Preserve 會處理對象循環引用，同時保持輸出的 JSON 的完整性，這樣就不會再產生循環引用的錯誤。請確保更新後的程式碼有正確處理循環引用的情況。
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            string serializedCoaches = JsonSerializer.Serialize(memberList, options);
            return memberList;
        }


        // PUT: api/MembersDTO/5
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

        // POST: api/MembersDTO
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

        // DELETE: api/MembersDTO/5
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

        private bool MemberExists(int id)
        {
            return (_context.Members?.Any(e => e.MemberId == id)).GetValueOrDefault();
        }
    }
}
