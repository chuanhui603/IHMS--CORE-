using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IHMS.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace IHMS.Controllers.APIcontrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesDTOController : ControllerBase
    {
        private readonly IhmsContext _context;

        public CoursesDTOController(IhmsContext context)
        {
            _context = context;
        }

        // GET: api/CoursesDTO
        [HttpGet]
        public  IEnumerable<Course> GetCourses()
        {
          
            return  _context.Courses;
        }

        
        [Route("~/api/[controller]/GetCourseByOrderid/{orderId:int}")] 
       // GET: api/CoursesDTO/3
       [HttpGet("GetCourseByOrderid/{orderId}")]
        public async Task<List<Course>> GetCourseByOrderid(int orderId)
        {
            List<OrderDetail> orderDetails = await _context.OrderDetails.Where(od => od.OrderId == orderId).ToListAsync();
            // 使用 Include 來避免嵌套迴圈的查詢
            List<Course> courseList = await _context.OrderDetails
                    .Where(od => od.OrderId == orderId)
                    .SelectMany(od => _context.Schedules
                        .Where(schedule => schedule.ScheduleId == od.ScheduleId)
                        .SelectMany(schedule => _context.Courses
                            .Where(course => course.CourseId == schedule.CourseId)                          
                            
                        )
                    )
                    .ToListAsync();

            //資料庫重複關聯問題
            //在序列化時，ReferenceHandler.Preserve 會處理對象循環引用，同時保持輸出的 JSON 的完整性，這樣就不會再產生循環引用的錯誤。請確保更新後的程式碼有正確處理循環引用的情況。
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            string serializedCoaches = JsonSerializer.Serialize(courseList, options);
            return courseList;
        }

        // PUT: api/CoursesDTO/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.CourseId)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
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

        // POST: api/CoursesDTO
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
          if (_context.Courses == null)
          {
              return Problem("Entity set 'IhmsContext.Courses'  is null.");
          }
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.CourseId }, course);
        }

        // DELETE: api/CoursesDTO/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            if (_context.Courses == null)
            {
                return NotFound();
            }
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return (_context.Courses?.Any(e => e.CourseId == id)).GetValueOrDefault();
        }
    }
}
