using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IHMS.Models;
using IHMS.DTO;
using System.Numerics;


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




        // GET: api/Plans
        [Route("~/api/[controller]/member/{memberid:int}/{nums:int}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlansSideBarDTO>>> GetPlans(int memberid, int nums)
        {
            if (_context.Plans == null)
            {
                return NotFound();
            }
            var res = _context.Plans.Where(p => p.MemberId == memberid).OrderByDescending(p => p.RegisterDate).Take(nums).Select(p => new PlansSideBarDTO
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

        // GET: api/Plans
        [Route("~/api/[controller]/member/{memberid:int}/search/{search}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlansSideBarDTO>>> GetPlans(int memberid, string search)
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
                Registerdate =dt.Registerdate.ToString(),
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
                Description = p.Description,
                SportDetailId = p.SportDetailId,
                Sname = p.Sname,
                Sporttime = p.Sporttime,
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

        // PUT: api/Plans/5
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
        public async Task<IActionResult> PutSportDetail( [FromForm] SportDetailDTO sportDTO, [FromForm] List<IFormFile> Img)
        {
            if (sportDTO.SportDetailId ==null)
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
                Description = sportDTO.Description,
                Registerdate = Convert.ToDateTime(sportDTO.Registerdate),
                Type = sportDTO.Type,
            };

            _context.Entry(sport).State = EntityState.Modified;

            //建立圖片路徑
            var webRootPath = _webHostEnvironment.WebRootPath;
            var imageDirectory = Path.Combine(webRootPath, "SportImg");
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
                    //判斷資料庫中是否sportdetail有其他圖片 如果有則清除全部相關圖片跟資料庫資料後再存取資料
                    var imgs = _context.SportImgs.Where(img => img.SportDetailId == sport.SportDetailId).ToList();
                    if(imgs.Count > 0)
                    {
                        foreach(var img in imgs)
                        {
                            string filepath= Path.Combine(imageDirectory, img.Img);
                            if (System.IO.File.Exists(filepath))
                            {
                                System.IO.File.Delete(filepath);
                            }
                            _context.SportImgs.Remove(img);
                        }
                        //存取資料
                        string fileName = $"{sportDTO.SportDetailId}_{Guid.NewGuid()}{Path.GetExtension(Image.FileName)}";
                        string filePath = Path.Combine(imageDirectory, fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            Image.CopyTo(stream);
                        }
                        SportImg saveimg = new SportImg
                        {
                            SportDetailId = sportDTO.SportDetailId,
                            Img = fileName,
                        };
                        _context.SportImgs.Add(saveimg);
                    }else
                    {
                        //存取資料
                        string fileName = $"{sportDTO.SportDetailId}_{Guid.NewGuid()}{Path.GetExtension(Image.FileName)}";
                        string filePath = Path.Combine(imageDirectory, fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            Image.CopyTo(stream);
                        }
                        SportImg saveimg = new SportImg
                        {
                            SportDetailId = sportDTO.SportDetailId,
                            Img = fileName,
                        };
                        _context.SportImgs.Add(saveimg);
                    }               
                }
            }

         
      
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
        public async Task<ActionResult<Plan>> PostPlan(CreatePlanDTO dto)
        {

            if (_context.Plans == null)
            {
                return Problem("Entity set 'IhmsContext.Plans' is null.");
            }
            Plan plan = new Plan
            {
                Pname = dto.pname,
                MemberId = dto.memberid,
                Weight = dto.weight,
                BodyPercentage = dto.Bmi,
                EndDate = dto.endDate,
                RegisterDate = DateTime.Now,
                Description = dto.description,
            };
            _context.Plans.Add(plan);
            await _context.SaveChangesAsync();

            return plan;
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
