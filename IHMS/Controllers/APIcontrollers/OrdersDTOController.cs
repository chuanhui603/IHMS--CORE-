using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IHMS.Models;



namespace IHMS.Controllers.APIcontrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersDTOController : ControllerBase
    {
        private readonly IhmsContext _context;

        public OrdersDTOController(IhmsContext context)
        {
            _context = context;
        }

        // GET: api/OrdersDTO
        //顯示某會員全部的資料
        [HttpGet]
        public IEnumerable<Order> GetOrders()
        {
         
            return _context.Orders;
        }

        // GET: api/OrdersDTO/3
        //顯示某會員的某筆資料
        //拿到orderid去找到orderdetail裡的orderid
        //會找到多筆1對1的orderdetail與scheduleid        
        //物件再回傳給order
        [HttpGet("{orderId}")]
        //Task執行續搭配async
        public async Task<List<Coach>>  GetOrder(int orderId)
        {
            List<Coach> coachesList = new List<Coach>();
            //使用訂單ID和訂單詳細資料ID查詢相關的訂單詳細資料

            List<OrderDetail> orderDetails = await _context.OrderDetails.Where(od => od.OrderId == orderId).ToListAsync();
            foreach(OrderDetail orderDetail in orderDetails)
            {                
                List<Schedule> schedules = await _context.Schedules.Where(od => od.ScheduleId == orderDetail.ScheduleId).ToListAsync();
                foreach (Schedule schedule in schedules)
                {
                    List<Course> courses = await _context.Courses.Where(od => od.CourseId == schedule.CourseId).ToListAsync();
                    foreach (Course course in courses)
                    {
                        List<Coach> coaches = await _context.Coaches.Where(od => od.CoachId == course.CoachId).ToListAsync();
                        foreach(Coach coach in coaches)
                        {
                            coachesList.AddRange(coaches);
                        }
                    }
                }             
                              
            }
            return coachesList;


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


        // PUT: api/OrdersDTO/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // 修改->先找到會員的id值,在選要修改哪一筆資料
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/OrdersDTO
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
          if (_context.Orders == null)
          {
              return Problem("Entity set 'IhmsContext.Orders'  is null.");
          }
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        // DELETE: api/OrdersDTO/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
