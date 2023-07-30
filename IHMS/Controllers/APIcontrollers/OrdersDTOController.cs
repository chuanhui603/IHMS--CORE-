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
                        List<Coach> coaches = await _context.Coaches.Where(od => od.CoachId == course.CoachContactId).ToListAsync();
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

        public IhmsContext Get_context()
        {
            return _context;
        }

        // POST: api/OrdersDTO
        //create
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<string> PostOrder(OrderRequest orderRequest, IhmsContext _context)
        {
            if( _context.Orders == null || _context.Members == null)
            { 
                return "新增失敗";
            }

            // 從資料庫中取得最後一筆訂單編號
            string lastOrderNumber = _context.Orders.Select(o => o.Ordernumber).OrderByDescending(o => o).FirstOrDefault();

            // 解析日期部分和數字部分
            string currentDate = DateTime.Now.ToString("yyyyMMdd");
            int lastNumber = 0;
            if (!string.IsNullOrEmpty(lastOrderNumber) && lastOrderNumber.Length >= 12)
            {
                lastNumber = int.Parse(lastOrderNumber.Substring(8));
            }

            // 產生新的數字部分
            int newNumber = lastNumber + 1;

            // 將日期和數字組合成新的訂單編號
            string newOrderNumber = currentDate + newNumber.ToString("D4");

            Member targeMebmer = _context.Members.FirstOrDefault(m => m.MemberId == orderRequest.MemberId);


            Order Ord = new Order
            {
                Ordernumber = newOrderNumber,
                Pointstotal = orderRequest.Pointstotal,
                State = "購買成功",
                Reason = "",
                Member= targeMebmer,
            };
            _context.Orders.Add(Ord);
            await _context.SaveChangesAsync();

            return "新增訂單成功";
        }

        // DELETE: api/OrdersDTO/5
        [HttpDelete("{id}")]
        public async Task<string> DeleteOrder(int id)
        {
            if (_context.Orders == null)
            {
                return "刪除訂單失敗";
            }
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return "刪除訂單失敗";
            }

            try
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return "刪除訂單關聯記錄失敗!";
            }

            return "刪除訂單記錄成功!";
        }

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
