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
        public  IEnumerable<PointRecord> GetPointRecords()
        {
          
            return  _context.PointRecords;
        }

        // GET: api/PointRecordsDTO/1
        //顯示某會員的某筆資料
        [HttpGet("{id}")]
        public async Task<PointRecord> GetPointRecord(int id)
        {          
            var pointRecord = await _context.PointRecords.FindAsync(id);         

            return pointRecord;
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
        public async Task<string> PostPointRecord(PointRecordRequest pointRecordRequest , IhmsContext _context)
        {
            if (_context.PointRecords == null || _context.Members == null)
            {
                return "新增失敗";
            }

            Member targeMebmer = _context.Members.FirstOrDefault(m => m.MemberId == pointRecordRequest.MemberId);

            PointRecord  Pointre = new PointRecord
            {
                Count = pointRecordRequest.Count,
                BankNumber = pointRecordRequest.BankNumber,             
                Member = targeMebmer,
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
    }
}
