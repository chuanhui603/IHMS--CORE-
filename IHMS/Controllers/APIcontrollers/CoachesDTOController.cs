using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IHMS.Models;
using IHMS.ViewModel.DTO;

namespace IHMS.Controllers.APIcontrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachesDTOController : ControllerBase
    {
        private readonly IhmsContext _context;

        public CoachesDTOController(IhmsContext context)
        {
            _context = context;
        }

        // GET: api/CoachesDTO
        [HttpGet]
        public  IEnumerable<Coach> GetCoaches()
        {
          
            return  _context.Coaches;
        }

        // GET: api/CoachesDTO/7
        [HttpGet("GetCoachByOrderid/{orderId}")]
        public async Task<List<Coach>> GetCoachByOrderid(int orderId)
        {
            List<OrderDetail> orderDetails = await _context.OrderDetails.Where(od => od.OrderId == orderId).ToListAsync();
            // 使用 Include 來避免嵌套迴圈的查詢
            List<Coach> coachesList = await _context.OrderDetails
                    .Where(od => od.OrderId == orderId)
                    .SelectMany(od => _context.Schedules
                        .Where(schedule => schedule.ScheduleId == od.ScheduleId)
                        .SelectMany(schedule => _context.Courses
                            .Where(course => course.CourseId == schedule.CourseId)
                            .SelectMany(course => _context.Coaches
                                .Where(coach => coach.CoachId == course.CoachContactId)
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

            string serializedCoaches = JsonSerializer.Serialize(coachesList, options);
            return coachesList;
        }



        


        // PUT: api/CoachesDTO/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoach(int id, Coach coach)
        {
            if (id != coach.CoachId)
            {
                return BadRequest();
            }

            _context.Entry(coach).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoachExists(id))
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

        // POST: api/CoachesDTO
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Coach>> PostCoach(Coach coach)
        {
          if (_context.Coaches == null)
          {
              return Problem("Entity set 'IhmsContext.Coaches'  is null.");
          }
            _context.Coaches.Add(coach);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoach", new { id = coach.CoachId }, coach);
        }

        // DELETE: api/CoachesDTO/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoach(int id)
        {
            if (_context.Coaches == null)
            {
                return NotFound();
            }
            var coach = await _context.Coaches.FindAsync(id);
            if (coach == null)
            {
                return NotFound();
            }

            _context.Coaches.Remove(coach);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CoachExists(int id)
        {
            return (_context.Coaches?.Any(e => e.CoachId == id)).GetValueOrDefault();
        }
    }
}
