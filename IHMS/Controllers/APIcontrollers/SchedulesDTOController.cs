using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IHMS.Models;
using IHMS.ViewModel.DTO;

namespace IHMS.Controllers.APIcontrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesDTOController : ControllerBase
    {
        private readonly IhmsContext _context;

        public SchedulesDTOController(IhmsContext context)
        {
            _context = context;
        }

        // GET: api/SchedulesDTO
        [HttpGet]
        public IEnumerable<Schedule>GetSchedules()
        {
          
            return  _context.Schedules;
        }

        // GET: api/SchedulesDTO/5
        [HttpGet("GetOrderDetailByOrderid/{orderId}")]
        public async Task<ScheduleDTO> GetSchedule(int orderId)
        {

            //使用訂單ID和訂單詳細資料ID查詢相關的訂單詳細資料
           
            var ScheduleId =  _context.OrderDetails.FirstOrDefault(od => od.OrderId == orderId).ScheduleId;
            var data = _context.Schedules.Where(s => s.ScheduleId == ScheduleId).FirstOrDefault();
            var scheduleList = new ScheduleDTO
            {
                CourseTime = data.CourseTime
            };
            //foreach (OrderDetail orderDetail in orderDetails)
            //{
            //    List<Schedule> schedules = await _context.Schedules.Where(od => od.ScheduleId == orderDetail.ScheduleId).ToListAsync();
            //    foreach (Schedule schedule in schedules)
            //    {
            //        scheduleList.AddRange(schedules);

            //        //List<Course> courses = await _context.Courses.Where(od => od.CourseId == schedule.CourseId).ToListAsync();
            //        //foreach (Course course in courses)
            //        //{
            //        //    List<Coach> coaches = await _context.Coaches.Where(od => od.CoachId == course.CoachId).ToListAsync();
            //        //    foreach (Coach coach in coaches)
            //        //    {
            //        //        scheduleList.AddRange(coaches);
            //        //    }
            //        //}
            //    }


            return scheduleList;


            // 使用訂單詳細資料中的scheduleid查詢相關的Schedule資料

            //var schedules = await _context.Schedules.FirstOrDefaultAsync(s => s.ScheduleId == orderDetail.ScheduleId);

            // var schedules = await _context.Schedules.Where(s => s.ScheduleId == orderDetail.ScheduleId).ToListAsync();

            //在foreach迴圈一筆一筆拿出資料
            //if (schedules != null)
            // {
            // foreach (Schedule data in schedules)
            // {
            //order.ScheduleInfo = data.ScheduleInfo;
            //    var test =  data.ScheduleId;
            // order.ScheduleInfo += data.ScheduleId + ", ";
            //   }
            //   }

            //物件再回傳給order
            //return schedules[0];
        }



        // PUT: api/SchedulesDTO/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchedule(int id, Schedule schedule)
        {
            if (id != schedule.ScheduleId)
            {
                return BadRequest();
            }

            _context.Entry(schedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExists(id))
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

        // POST: api/SchedulesDTO
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Schedule>> PostSchedule(Schedule schedule)
        {
          if (_context.Schedules == null)
          {
              return Problem("Entity set 'IhmsContext.Schedules'  is null.");
          }
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSchedule", new { id = schedule.ScheduleId }, schedule);
        }

        // DELETE: api/SchedulesDTO/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            if (_context.Schedules == null)
            {
                return NotFound();
            }
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScheduleExists(int id)
        {
            return (_context.Schedules?.Any(e => e.ScheduleId == id)).GetValueOrDefault();
        }
    }
}
