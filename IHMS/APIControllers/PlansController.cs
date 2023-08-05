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
using System.Composition;

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
        //取得當前會員的計畫規劃
        [Route("~/api/[controller]/member/{memberid:int}")]
        [HttpGet]
        public async Task<ActionResult<Plan>> GetPlan(int memberid)
        {
            if (_context.Plans == null)
            {
                return NotFound();
            }
            var res = _context.Plans.Where(p => p.MemberId == memberid).Select(p => new PlanDTO
            {
                PlanId = p.PlanId,
                Age = p.Age,
                RegisterDate = p.RegisterDate,
                Bmr = p.Bmr,
                BodyPercentage = p.BodyPercentage,
                Height = p.Height,
                Tdee = p.Tdee,
                Times = p.Times,
                Type = p.Type,
                MemberId = p.MemberId,
                Weight = p.Weight,
                Gender =p.Gender,

            }).FirstOrDefault();

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
        public async Task<ActionResult<DateTime>> GetSportDate(int planid)
        {
            if (_context.Plans == null)
            {
                return NotFound();
            }
            var res = _context.Sports.Where(p => p.PlanId == planid).OrderByDescending(s => s.Createdate).FirstOrDefault();
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

        [Route("~/api/[controller]/sportdetail/list/{sportid:int}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SportDetailDTO>>> GetSportDetailList(int sportid)
        {
            if (_context.SportDetails == null)
            {
                return NotFound();
            }
            var res = _context.SportDetails.OrderBy(p => p.Registerdate).Where(p => p.SportId == sportid).Select(p => new SportDetailDTO
            {
                SportDetailId = p.SportDetailId,
                SportId = p.SportId,
                Sname=p.Sname,
                Sets = p.Sets,
                Frequency = p.Frequency,
                Isdone = p.Isdone,
                Registerdate = p.Registerdate,
                Time = p.Time,
                Timelong = p.Timelong,
                Type = p.Type,
                Calories = p.Calories,
            });

            if (res == null)
            {
                return NotFound();
            }
            else
            {
                return  Ok(res);
            }

        }


        [Route("~/api/[controller]/sportdetail/sum/{sportid:int}")]
        [HttpGet]
        public async Task<ActionResult<int>> GetSportDetailSum(int sportid)
        {
            if (_context.SportDetails == null)
            {
                return NotFound();
            }
            var sum = _context.SportDetails.Where(s => s.SportId == sportid).Sum(sd => sd.Calories);

            if (sum == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(sum);
            }

        }




        //// GET: api/plans/sportdetail/${sportid}/search/${search.value}
        [Route("~/api/[controller]/sportdetail/{sportid:int}/search/{search}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SportDetailDTO>>> GetSportDetailSearch(int sportid, string search)
        {
            if (_context.Plans == null)
            {
                return NotFound();
            }
            var res = _context.SportDetails.OrderBy(p => p.Registerdate).Where(p => p.SportId == sportid && p.Sname.Contains($"{search}")).Select(p => new SportDetailDTO
            {
               Sname = p.Sname,
               SportDetailId = p.SportDetailId,
               SportId = p.SportId,
               Frequency = p.Frequency,
               Isdone = p.Isdone,
               Registerdate  =p.Registerdate,
               Time = p.Time,
               Timelong = p.Timelong,
               Type = p.Type,
               Sets = p.Sets,
               Calories =p.Calories,
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

        //取得單筆sportdetail資料
        [Route("~/api/[controller]/sportdetail/{sportdetailid:int}")]
        [HttpGet]
        public async Task<ActionResult<SportDetailDTO>> GetSportDetail(int sportdetailid)
        {
            if (_context.SportDetails == null)
            {
                return NotFound();
            }
            var res = _context.SportDetails.Where(p => p.SportDetailId == sportdetailid).Select(p => new SportDetailDTO
            {
                SportDetailId = p.SportDetailId,
                SportId = p.SportId,
                Sname = p.Sname,
                Sets = p.Sets,
                Frequency = p.Frequency,
                Isdone = p.Isdone,
                Registerdate = p.Registerdate,
                Time = p.Time,
                Timelong = p.Timelong,
                Type = p.Type,
                Calories =p.Calories,
                
                
            }).FirstOrDefault();

            if (res == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(res);
            }

        }
        //api/plans/dietdetail/list/{dietid:int}
        //取得dietdetail資料
        [Route("~/api/[controller]/dietdetail/list/{dietid:int}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DietDetailDTO>>> GetDietDetailList(int dietid)
        {
            if (_context.DietDetails == null)
            {
                return NotFound();
            }
            var res = _context.DietDetails.Where(p => p.DietId == dietid).Select(d => new DietDetailDTO
            {
               DietDetailId = d.DietDetailId,
               Dname = d.Dname,
               Calories = d.Calories,
               Registerdate =d.Registerdate,
               Type = d.Type,    
               Decription = d.Decription,
            });

            if (res == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(res);
            }

        }

        //api/plans/diet/sum/{dietid:int}
        //取得dietdetail資料
        [Route("~/api/[controller]/diet/sum/{dietid:int}")]
        [HttpGet]
        public async Task<ActionResult<int>> GetDietSum(int dietid)
        {
            if (_context.DietDetails == null)
            {
                return NotFound();
            }
            var res = _context.DietDetails.Where(p => p.DietId == dietid).Sum(dd => dd.Calories);

            if (res == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(res);
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
                Isdone = p.Isdone,
                Calories = p.Calories,
                Sets = p.Sets,
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

        // PUT: api/Plans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("~/api/[controller]/edit")]
        [HttpPut]
        public async Task<IActionResult> PutPlan(PlanDTO plan)
        {

            var res = _context.Plans.Where(p => p.PlanId == plan.PlanId).Select(p => new Plan
            {
                PlanId = plan.PlanId,
                BodyPercentage = plan.BodyPercentage,
                Age = plan.Age,
                Bmr = plan.Bmr,
                Height = plan.Height,
                MemberId = plan.MemberId,
                RegisterDate = DateTime.Now,
                Type = plan.Type,
                Tdee = plan.Tdee,
                Times = plan.Times,
                Weight = plan.Weight,
                Gender = plan.Gender,
                
            }).FirstOrDefault();
            _context.Entry(res).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanExists(plan.PlanId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
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
                Registerdate = Convert.ToDateTime(dietDTO.Registerdate),
                
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
        [Route("~/api/[controller]/sportdetail/complete/{detailid:int}")]
        [HttpPut]
        public async Task<IActionResult> PutSportChangeTrue(int detailid)
        {
          
            //處理文字
            var res = _context.SportDetails.Where(s=>s.SportDetailId == detailid).FirstOrDefault();
            res.Isdone = true;
            _context.Entry(res).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanExists(detailid))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
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
                Registerdate =DateTime.Now,
                Type = sportDTO.Type,
                Time = sportDTO.Time,
                Timelong = sportDTO.Timelong,
                Isdone = sportDTO.Isdone,
                Calories = sportDTO.Calories,
                Sets = sportDTO.Sets,
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

            return Ok();
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
                Age = dto.Age,
                MemberId = dto.MemberId,
                Weight = dto.Weight,
                BodyPercentage = dto.BodyPercentage,
                Bmr = dto.Bmr,
                RegisterDate = DateTime.Now,
                Tdee = dto.Tdee,
                Type = dto.Type,
                Times = dto.Times,
                Gender = dto.Gender,
            };
            _context.Plans.Add(plan);
            await _context.SaveChangesAsync();

            return Ok(plan);
        }

        // POST: api/Plans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("~/api/[controller]/SportDetail")]
        [HttpPost]
        public async Task<ActionResult<SportDetail>> PostSportDetail([FromForm] SportDetailDTO dto)
        {

            if (_context.SportDetails == null)
            {
                return Problem("Entity set 'IhmsContext.SportDetails' is null.");
            }
            SportDetail sport = new SportDetail
            {
                SportId = 1,
                Frequency = dto.Frequency,
                Isdone = false,
                Sname = dto.Sname,
                Registerdate = DateTime.Now,
                Time = dto.Time,
                Timelong = dto.Timelong,
                Type = dto.Type,
                Sets = dto.Sets,
                Calories = dto.Calories,
                
            };
            _context.SportDetails.Add(sport);
            await _context.SaveChangesAsync();

            return Ok(sport);
        }
        //DietDetail
        // PUT: api/Plans/diets/{dietsid}/edit
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("~/api/[controller]/dietdetail")]
        [HttpPost]
        public async Task<IActionResult> PostDietDetail([FromForm] DietDetailDTO dietDTO, [FromForm] List<IFormFile> Img)
        {
            //處理資料
            DietDetail diet = new DietDetail
            {
                DietId = dietDTO.DietId,
                Dname = dietDTO.Dname,
                Type = dietDTO.Type,
                Decription = dietDTO.Decription,
                Calories = dietDTO.Calories,
                Registerdate = Convert.ToDateTime(dietDTO.Registerdate)
            };

            _context.DietDetails.Add(diet);
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
            int dietdetailid = _context.DietDetails.OrderByDescending(p => p.DietDetailId).FirstOrDefault().DietDetailId;
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

                    //存取資料
                    string fileName = $"{dietdetailid}_{Guid.NewGuid()}{Path.GetExtension(Image.FileName)}";
                    string filePath = Path.Combine(imageDirectory, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Image.CopyTo(stream);
                    }
                    DietImg saveimg = new DietImg
                    {
                        DietDetailId = dietdetailid,
                        Img = fileName,
                    };
                    _context.DietImgs.Add(saveimg);

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

            return Ok();
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

        // DELETE: api/Plans/5
        [Route("~/api/[controller]/sportdetail/delete/{detailId:int}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSportDetail(int detailId)
        {         
            var detail = await _context.SportDetails.FindAsync(detailId);
        
            if (detail == null)
            {
                return NotFound();
            }
            _context.SportDetails.Remove(detail);
            await _context.SaveChangesAsync();

            return Ok();
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
