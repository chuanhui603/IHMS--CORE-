using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IHMS.Models;
using IHMS.ViewModel.DTO;
using IHMS.DTO;

namespace IHMS.Controllers.APIcontrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointRecordsDTOController : ControllerBase
    {
        private readonly IhmsContext _context;

        public PointRecordsDTOController(IhmsContext context)
        {
            _context = context;
        }

        // GET: api/PointRecordsDTO
        //顯示某會員全部的資料
        [HttpGet]
        public  IEnumerable<PointRecord> GetPointRecords(int memberid)
        {
          


            return  _context.PointRecords;
        }

        // GET: api/PointRecordsDTO/1
        //顯示某會員的某筆資料
        //取點數資料
        [HttpGet("{MemberId}")]
        public async Task<int> GetPointsum(int MemberId)
        {
            var coursecost = (
                from o in _context.Orders
                join od in _context.OrderDetails on o.OrderId equals od.OrderId
                join m in _context.Members on o.MemberId equals m.MemberId
                join s in _context.Schedules on od.ScheduleId equals s.ScheduleId
                join c in _context.Courses on s.CourseId equals c.CourseId
                where o.MemberId == MemberId
                select c.CourseTotal                
            ).Sum();




            var currentp = _context.PointRecords
                   .Where(pr => pr.MemberId == MemberId)
                   .Sum(pr => pr.Count) * 500;
            //var total = _context.PointRecords.Select(s => s.Count).ToList().Sum()*500;
            //var cost = _context.Courses.Select(s => s.CourseTotal).Sum();

            return currentp - coursecost;
  

        }

        // PUT: api/PointRecordsDTO/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPointRecord(int id, PointRecord pointRecord)
        {
            if (id != pointRecord.PointrecordId)
            {
                return BadRequest();
            }

            _context.Entry(pointRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PointRecordExists(id))
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

        // POST: api/PointRecordsDTO
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<string> PostPointRecord(PointRecordRequest pointRecordRequest)
        {
            if ( _context.Members == null)
            {
                return "新增失敗";
            }

            Member targeMebmer = _context.Members.FirstOrDefault(m => m.MemberId == pointRecordRequest.MemberId);

            Random random = new Random(Guid.NewGuid().GetHashCode());            
            int bankNumber = random.Next(10000000, 99999999);


            PointRecord  Pointre = new PointRecord
            {
                Count = pointRecordRequest.Count,
                BankNumber = bankNumber,             
                Member = targeMebmer,
                Createtime=DateTime.Now

            };
            _context.PointRecords.Add(Pointre);
            await _context.SaveChangesAsync();

            return "新增成功";
        }

        // DELETE: api/PointRecordsDTO/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePointRecord(int id)
        {
            if (_context.PointRecords == null)
            {
                return NotFound();
            }
            var pointRecord = await _context.PointRecords.FindAsync(id);
            if (pointRecord == null)
            {
                return NotFound();
            }

            _context.PointRecords.Remove(pointRecord);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PointRecordExists(int id)
        {
            return (_context.PointRecords?.Any(e => e.PointrecordId == id)).GetValueOrDefault();
        }

        //測試產生亂數api

        // POST: api/testPointRecordsDTO
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public string testPostPointRecord(PointRecordRequest pointRecordRequest, IhmsContext _context)
        //{
        //    //if (_context.PointRecords == null || _context.Members == null)
        //    //{
        //    //    return "新增失敗";
        //    //}
            
        //    Random random = new Random();
        //    int bankNumber = random.Next(10000000, 99999999);

        //    PointRecord Pointre = new PointRecord
        //    {                
        //        BankNumber = bankNumber,
                
        //    };
            //_context.PointRecords.Add(Pointre);
            //await _context.SaveChangesAsync();

        //    return "新增成功";
        //}

    }
}
