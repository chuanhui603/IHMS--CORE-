using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IHMS.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using IHMS.DTO;
using System.Numerics;
using Newtonsoft.Json;

namespace IHMS.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlansController : ControllerBase
    {
        private readonly IhmsContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public PlansController(IhmsContext context, IWebHostEnvironment webHostEnvironment)
        {

            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        #region Get




        // GET: api/plans/member/{memberid}/{nums}
        [Route("~/api/[controller]/member/{memberid:int}")]
        [HttpGet]
        public async Task<ActionResult<Plan>> GetPlans(int memberid)
        {
            if (_context.Plans == null)
            {
                return NotFound();
            }
            var res = _context.Plans.Where(p => p.MemberId == memberid).FirstOrDefault();
          
            if (res == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(res);
            }

        }


        // GET: api/plans/sport/{planid}
        //取得運動更新的最新日期
        [Route("~/api/[controller]/sport/{planid:int}")]
        [HttpGet]
        public async Task<ActionResult<DateTime>> GetSport(int planid)
        {
            if (_context.Plans == null)
            {
                return NotFound();
            }
            var res = _context.Sports.Where(p => p.PlanId == planid).OrderByDescending(s=>s.Createdate).FirstOrDefault();
            DateTime date = res.Createdate;

            if (res == null)
            {
                return NotFound();
            }
            else
            {
                return date;
            }

        }

        //// GET: api/plans/member/{memberid:int}/search/{search}
        //[Route("~/api/[controller]/member/{memberid:int}/search/{search}")]
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<PlansSideBarDTO>>> GetPlans(int memberid, string search)
        //{
        //    if (_context.Plans == null)
        //    {
        //        return NotFound();
        //    }
        //    var res = _context.Plans.OrderByDescending(p => p.RegisterDate).Where(p => p.MemberId == memberid && p.Pname.Contains($"{search}")).Select(p => new PlansSideBarDTO
        //    {
        //        PlanId = p.PlanId,
        //        Pname = p.Pname,
        //        RegisterDate = p.RegisterDate,
        //        EndDate = p.EndDate,
        //    });

        //    if (res == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        return await res.ToListAsync();
        //    }

        //}

        // GET: api/Plans/diet/{dietid}/dietDetail
        [Route("~/api/[controller]/diet/{dietid:int}/dietDetail")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DietDetailDTO>>> GetDietDetail(int dietid)
        {
            if (_context.Diets == null && _context.DietDetails == null)
            {
                return NotFound();
            }
            var res = _context.DietDetails.Where(dt => dt.DietId == dietid).OrderByDescending(p => p.Registerdate).Select(dt => new DietDetailDTO
            {

                DietDetailId = dt.DietDetailId,
                Decription = dt.Decription,
                Dname = dt.Dname,
                Registerdate = dt.Registerdate.ToString(),
                Type = dt.Type,
                Calories = dt.Calories,
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

        // GET: api/Plans/Sport/{SportId}/Sportdetail
        [Route("~/api/[controller]/Sport/{SportId:int}/Sportdetail")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SportDetailDTO>>> GetSportDetails(int SportId)
        {
            if (_context.Plans == null)
            {
                return NotFound();
            }
            var res = _context.SportDetails.Where(p => p.SportId == SportId).Select(p => new SportDetailDTO
            {
                SportId = p.SportId,
                SportDetailId = p.SportDetailId,
                Sname = p.Sname,
                Timelong = p.Timelong,
                Time = p.Time,
                Frequency = p.Frequency,
                Registerdate = p.Registerdate,
                Type = p.Type,
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

        // GET: api/diet/{dietid:int}/dietDetail/{dietDetail:int}

        #endregion


        #region Put

        // PUT: api/Plans/{planid}
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlan(int id, PlanDTO plan)
        {
            if (id != plan.PlanId)
            {
                return BadRequest();
            }
            var res = _context.Plans.Where(p => p.PlanId == id).Select(p => new Plan
            {
                PlanId = p.PlanId,
                BodyPercentage = p.BodyPercentage,
                Age = p.Age,
                Bmr = p.Bmr,
                Height = p.Height,
                MemberId = p.MemberId,
                RegisterDate = DateTime.Now,
                Type = p.Type,
                Tdee = p.Tdee,
                Times = p.Times,
                Weight = p.Weight,  

            });
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

        //DietDetail
        // PUT: api/Plans/diets/{dietsid}/edit
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("~/api/[controller]/dietdetail/edit")]
        [HttpPut]
        public async Task<IActionResult> PutDietDetail([FromForm] DietDetailDTO dietDTO, [FromForm] List<IFormFile> Img)
        {
            if (dietDTO.DietDetailId == null)
            {
                return BadRequest();
            }
            //處理文字
            DietDetail diet = new DietDetail
            {
                DietDetailId = dietDTO.DietDetailId,
                DietId = dietDTO.DietId,
                Dname = dietDTO.Dname,
                Type = dietDTO.Type,
                Decription = dietDTO.Decription,
                Calories = dietDTO.Calories,
                Registerdate = Convert.ToDateTime(dietDTO.Registerdate)
            };

            _context.Entry(diet).State = EntityState.Modified;

            //建立圖片路徑
            var webRootPath = _webHostEnvironment.WebRootPath;
            var imageDirectory = Path.Combine(webRootPath, "DietImg");
            //若路徑中沒有資料夾則建立資料夾
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }
            //處理圖片
            if (Img != null && Img.Count > 0)
            {
                foreach (var Image in Img)
                {
                    //判斷資料庫中是否dietdetail有其他圖片 如果有則清除全部相關圖片跟資料庫資料後再存取資料
                    var imgs = _context.DietImgs.Where(img => img.DietDetailId == diet.DietDetailId).ToList();
                    if (imgs.Count > 0)
                    {
                        foreach (var img in imgs)
                        {
                            string filepath = Path.Combine(imageDirectory, img.Img);
                            if (System.IO.File.Exists(filepath))
                            {
                                System.IO.File.Delete(filepath);
                            }
                            _context.DietImgs.Remove(img);
                        }
                        //存取資料
                        string fileName = $"{dietDTO.DietDetailId}_{Guid.NewGuid()}{Path.GetExtension(Image.FileName)}";
                        string filePath = Path.Combine(imageDirectory, fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            Image.CopyTo(stream);
                        }
                        DietImg saveimg = new DietImg
                        {
                            DietDetailId = dietDTO.DietDetailId,
                            Img = fileName,
                        };
                        _context.DietImgs.Add(saveimg);
                    }
                    else
                    {
                        //存取資料
                        string fileName = $"{dietDTO.DietDetailId}_{Guid.NewGuid()}{Path.GetExtension(Image.FileName)}";
                        string filePath = Path.Combine(imageDirectory, fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            Image.CopyTo(stream);
                        }
                        DietImg saveimg = new DietImg
                        {
                            DietDetailId = dietDTO.DietDetailId,
                            Img = fileName,
                        };
                        _context.DietImgs.Add(saveimg);
                    }
                }
            }



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanExists(dietDTO.DietDetailId))
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

        //SportDetail
        // PUT: api/Plans/sportdetail/{sportdetailid}/edit
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("~/api/[controller]/sportdetail/edit")]
        [HttpPut]
        public async Task<IActionResult> PutSportDetail([FromForm] SportDetailDTO sportDTO)
        {
            if (sportDTO.SportDetailId == null)
            {
                return BadRequest();
            }
            //處理文字
            SportDetail sport = new SportDetail
            {
                SportDetailId = sportDTO.SportDetailId,
                SportId = sportDTO.SportId,
                Sname = sportDTO.Sname,
                Frequency = sportDTO.Frequency,
                Registerdate = Convert.ToDateTime(sportDTO.Registerdate),
                Type = sportDTO.Type,
                Time = sportDTO.Time,
                Timelong = sportDTO.Timelong,
            };
            _context.Entry(sport).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanExists(sportDTO.SportDetailId))
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


        #endregion



        #region Post 

        // POST: api/Plans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPost]
        public async Task<ActionResult<Plan>> PostPlan(PlanDTO dto)
        {

            if (_context.Plans == null)
            {
                return Problem("Entity set 'IhmsContext.Plans' is null.");
            }
            Plan plan = new Plan
            {
                Age=dto.Age,
                MemberId = dto.MemberId,
                Weight = dto.Weight,
                BodyPercentage = dto.Bmi,
                Bmr = dto.Bmr,
                RegisterDate = DateTime.Now,
                Tdee = dto.Tdee,
                Type = dto.Type,
                Times = dto.Times,                
            };
            _context.Plans.Add(plan);
            await _context.SaveChangesAsync();

            return Ok(plan);
        }

        // POST: api/Plans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("~/api/[controller]/SportDetail")]
        [HttpPost]
        public async Task<ActionResult<SportDetail>> PostSportDetail(SportDetailDTO dto)
        {

            if (_context.SportDetails == null)
            {
                return Problem("Entity set 'IhmsContext.SportDetails' is null.");
            }
            SportDetail sport = new SportDetail
            {
               SportId = dto.SportId,
               Frequency = dto.Frequency,
               Isdone = false,
               Sname = dto.Sname,
               Registerdate = dto.Registerdate,
               Time = dto.Time,
               Timelong = dto.Timelong,
               Type = dto.Type,
            };
            _context.SportDetails.Add(sport);
            await _context.SaveChangesAsync();

            return Ok(sport);
        }


        #endregion



        #region Delete

        // DELETE: api/Plans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlan(int id)
        {
            DeletePlanDTO deleteitems = new DeletePlanDTO();

            if (_context.Plans == null)
            {
                return NotFound();
            }
            var plan = await _context.Plans.FindAsync(id);
            //移除plan的關聯資料
            deleteitems.diet = _context.Diets.Where(d => d.PlanId == id);
            deleteitems.sport = _context.Sports.Where(d => d.PlanId == id);
            deleteitems.water = _context.Water.Where(d => d.PlanId == id);
            deleteMethod(deleteitems);

            if (plan == null)
            {
                return NotFound();
            }
            _context.Plans.Remove(plan);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        #endregion


        #region function
        public void deleteMethod(DeletePlanDTO delete)
        {
            for (int i = 0; i < delete.deleteDataSet.Length; i++)
            {
                switch (delete.deleteDataSet[i])
                {
                    case "diet":

                        foreach (var diet in delete.diet)
                        {
                            var query = _context.DietDetails.Where(d => d.DietId == diet.DietId);
                            foreach (var detail in query)
                            {
                                _context.DietDetails.Remove(detail);
                            }
                            _context.Diets.Remove(diet);
                        }


                        break;
                    case "sport":

                        foreach (var sport in delete.sport)
                        {
                            var query = _context.SportDetails.Where(d => d.SportId == sport.SportId);
                            foreach (var detail in query)
                            {
                                _context.SportDetails.Remove(detail);
                            }
                            _context.Sports.Remove(sport);
                        }

                        break;
                    default:
                        foreach (var water in delete.water)
                        {
                            _context.Water.Remove(water);
                        }
                        break;
                }
            }
        }

        private bool PlanExists(int id)
        {
            return (_context.Plans?.Any(e => e.PlanId == id)).GetValueOrDefault();
        }

        #endregion


    }
}
