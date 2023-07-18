using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IHMS.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using IHMS.DTO;

namespace IHMS.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlansController : ControllerBase
    {
        private readonly IhmsContext _context;

        public PlansController(IhmsContext context)
        {
            _context = context;
        }



        // GET: api/Plans
        [Route("~/api/[controller]/member/{memberid:int}/{nums:int}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlansSideBarDTO>>> GetPlans(int memberid,int nums)
        {
            if (_context.Plans == null)
            {
                return NotFound();
            }
            var res = _context.Plans.Where(p => p.MemberId == memberid).OrderByDescending(p=>p.RegisterDate).Take(nums).Select(p => new PlansSideBarDTO
            {
                PlanId = p.PlanId,
                Pname = p.Pname,
                RegisterDate = p.RegisterDate,
                EndDate =p.EndDate,
            });
            
            if (res==null)
            {
                return NotFound();
            }
            else
            {
                return await res.ToListAsync();
            }

        }

        // GET: api/Plans
        [Route("~/api/[controller]/member/{memberid:int}/search/{search}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlansSideBarDTO>>> GetPlans(int memberid,string search)
        {
            if (_context.Plans == null)
            {
                return NotFound();
            }
            var res = _context.Plans.OrderByDescending(p => p.RegisterDate).Where(p => p.MemberId == memberid && p.Pname.Contains($"{search}")).Select(p => new PlansSideBarDTO
            {
                PlanId = p.PlanId,
                Pname = p.Pname,
                RegisterDate = p.RegisterDate,
                EndDate = p.EndDate,
            });

            if (res == null)
            {
                return NotFound();
            }
            else
            {
                return await res.ToListAsync();
            }

        }


        // GET: api/Plans/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Plan>> GetPlan(int id)
        //{
        //  if (_context.Plans == null)
        //  {
        //      return NotFound();
        //  }
        //    var plan = await _context.Plans.FindAsync(id);

        //    if (plan == null)
        //    {
        //        return NotFound();
        //    }

        //    return plan;
        //}

        // PUT: api/Plans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlan(int id, Plan plan)
        {
            if (id != plan.PlanId)
            {
                return BadRequest();
            }

            _context.Entry(plan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanExists(id))
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

        // POST: api/Plans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Plan>> PostPlan(CreatePlanDTO dto)
        {
            
            if (_context.Plans == null)
            {
                return Problem("Entity set 'IhmsContext.Plans' is null.");
            }
            Plan plan = new Plan
            {
                Pname = dto.pname,
                MemberId =dto.memberid,
                Weight =dto.weight,
                BodyPercentage =dto.Bmi,
                EndDate =dto.endDate,             
                RegisterDate =DateTime.Now,
                Description = dto.description,
            };
            _context.Plans.Add(plan);
            await _context.SaveChangesAsync();

            return plan;
        }

        // DELETE: api/Plans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlan(int id)
        {
            if (_context.Plans == null)
            {
                return NotFound();
            }
            var plan = await _context.Plans.FindAsync(id);
            //var diet =  _context.Diets.Include("Plan").Where(d => d.PlanId == id);
            //var sport = _context.Sports.Include("Plan").Where(d => d.PlanId == id);
            //var water = _context.Water.Include("Plan").Where(d => d.PlanId == id);           
            if (plan == null)
            {
                return NotFound();
            }
            _context.Plans.Remove(plan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlanExists(int id)
        {
            return (_context.Plans?.Any(e => e.PlanId == id)).GetValueOrDefault();
        }
    }
}
