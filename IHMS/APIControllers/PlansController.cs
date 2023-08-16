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
using Humanizer;

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
        public async Task<ActionResult<Plan>> GetPlan(int? memberid)
        {
            if (memberid == null)
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
                Gender = p.Gender,
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
        public async Task<ActionResult<DateTime>> GetSportDate(int? planid)
        {
            if (planid == null)
            {
                return NotFound();
            }
            var res = _context.Sports.Where(p => p.PlanId == planid).OrderByDescending(s => s.Createdate).FirstOrDefault();
            if(res == null)
            {
                return NotFound();
            }             
            else
            {
                DateTime date = res.Createdate;
                return date;
            }

        }

        [Route("~/api/[controller]/sportdetail/list/{sportid:int}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SportDetailDTO>>> GetSportDetailList(int? sportid)
        {
            if (sportid == null)
            {
                return NotFound();
            }
            var res = _context.SportDetails.OrderBy(p => p.Registerdate).OrderBy(p => p.Isdone).Where(p => p.SportId == sportid).Select(p => new SportDetailDTO
            {
                SportDetailId = p.SportDetailId,
                SportId = p.SportId,
                Sname = p.Sname,
                Sets = p.Sets,
                Frequency = p.Frequency,
                Isdone = p.Isdone,
                Registerdate = p.Registerdate,
                Time = p.Time,
                Hour = p.Hour,
                Min = p.Min,
                Type = p.Type,
                Calories = p.Calories,
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


        [Route("~/api/[controller]/sportdetail/sum/{sportid:int}")]
        [HttpGet]
        public async Task<ActionResult<int>> GetSportDetailSum(int? sportid)
        {
            if (sportid == null)
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
        public async Task<ActionResult<IEnumerable<SportDetailDTO>>> GetSportDetailSearch(int? sportid, string search)
        {
            if (sportid == null)
            {
                return NotFound();
            }
            //照日期以及是否完成為順序排列data
            var res = _context.SportDetails.OrderBy(p => p.Registerdate).OrderBy(p => p.Isdone).Where(p => p.SportId == sportid && p.Sname.Contains($"{search}")).Select(p => new SportDetailDTO
            {
                Sname = p.Sname,
                SportDetailId = p.SportDetailId,
                SportId = p.SportId,
                Frequency = p.Frequency,
                Isdone = p.Isdone,
                Registerdate = p.Registerdate,
                Time = p.Time,
                Hour = p.Hour,
                Min = p.Min,
                Type = p.Type,
                Sets = p.Sets,
                Calories = p.Calories,
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
        public async Task<ActionResult<SportDetailDTO>> GetSportDetail(int? sportdetailid)
        {
            if (sportdetailid == null)
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
                Hour = p.Hour,
                Min = p.Min,
                Type = p.Type,
                Calories = p.Calories,
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
        public async Task<ActionResult<IEnumerable<DietDetailDTO>>> GetDietDetailList(int? dietid)
        {
            if (dietid == null)
            {
                return NotFound();
            }
            var res = _context.DietDetails.Where(p => p.DietId == dietid).Select(d => new DietDetailDTO
            {
                DietDetailId = d.DietDetailId,
                Dname = d.Dname,
                Calories = d.Calories,
                Registerdate = d.Registerdate,
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
        public async Task<ActionResult<int>> GetDietSum(int? dietid)
        {
            if (dietid == null)
            {
                return NotFound();
            }
            int? res = _context.DietDetails.Where(p => p.DietId == dietid).Sum(dd => dd.Calories);
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
        public async Task<ActionResult<IEnumerable<SportDetailDTO>>> GetSportDetails(int? SportId)
        {
            if (SportId == null)
            {
                return NotFound();
            }
            var res = _context.SportDetails.Where(p => p.SportId == SportId).Select(p => new SportDetailDTO
            {
                SportId = p.SportId,
                SportDetailId = p.SportDetailId,
                Sname = p.Sname,
                Hour = p.Hour,
                Min = p.Min,
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
        public async Task<IActionResult> PutPlan(PlanDTO planDTO)
        {
            if (planDTO == null)
            {
                return NotFound();
            }
            var plan = PlanAdd(planDTO);

            _context.Entry(plan).State = EntityState.Modified;

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
            if (dietDTO == null)
            {
                return NotFound();
            }
            //更改Detail
            var diet = DietAdd(dietDTO);
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
            if (Img.Count > 0)
            {
                foreach (var Image in Img)
                {
                    //判斷資料庫中是否dietdetail有其他圖片 如果有則清除全部相關圖片跟資料庫資料後再存取資料
                    var imgs = _context.DietImgs.Where(img => img.DietDetailId == diet.DietDetailId).ToList();
                    if (imgs.Count > 0)
                    {
                        foreach (var img in imgs)
                        {
                            //移除儲存資料夾內重複圖片
                            string filepath = Path.Combine(imageDirectory, img.Img);
                            if (System.IO.File.Exists(filepath))
                            {
                                System.IO.File.Delete(filepath);
                            }
                            _context.DietImgs.Remove(img);
                        }
                        //設定亂數名稱存取資料
                        string fileName = $"{dietDTO.DietDetailId}_{Guid.NewGuid()}{Path.GetExtension(Image.FileName)}";
                        string filePath = Path.Combine(imageDirectory, fileName);
                        saveimg(Image, filePath, fileName, dietDTO);
                        //using (var stream = new FileStream(filePath, FileMode.Create))
                        //{
                        //    Image.CopyTo(stream);
                        //}
                        //DietImg saveimg = new DietImg
                        //{
                        //    DietDetailId = dietDTO.DietDetailId,
                        //    Img = fileName,
                        //};
                        //_context.DietImgs.Add(saveimg);
                    }
                    else
                    {
                        //存取資料
                        string fileName = $"{dietDTO.DietDetailId}_{Guid.NewGuid()}{Path.GetExtension(Image.FileName)}";
                        string filePath = Path.Combine(imageDirectory, fileName);
                        saveimg(Image, filePath, fileName, dietDTO);
                        //using (var stream = new FileStream(filePath, FileMode.Create))
                        //{
                        //    Image.CopyTo(stream);
                        //}
                        //DietImg saveimg = new DietImg
                        //{
                        //    DietDetailId = dietDTO.DietDetailId,
                        //    Img = fileName,
                        //};
                        //_context.DietImgs.Add(saveimg);
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
        public async Task<IActionResult> PutSportChangeTrue(int? detailid)
        {
            if (detailid == null)
            {
                return NotFound();
            }
            //處理文字
            var res = _context.SportDetails.Where(s => s.SportDetailId == detailid).FirstOrDefault();
            if (res == null)
            {
                return NotFound();
            }
            else
            {
                res.Isdone = true;
                _context.Entry(res).State = EntityState.Modified;              
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanExists(res.SportDetailId))
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
            if (sportDTO == null)
            {
                return NotFound();
            }
            //處理文字
            var sport = SportAdd(sportDTO);
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
            if (dto == null)
            {
                return NotFound();
            }
            var plan = PlanAdd(dto);
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
            if (dto == null)
            {
                return NotFound();
            }
            SportDetail sport = SportAdd(dto);
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
            if (dietDTO == null)
            {
                return NotFound();
            }
            //處理資料
            var diet = DietAdd(dietDTO);
            _context.DietDetails.Add(diet);          
            await _context.SaveChangesAsync();

            //處理圖片
            int dietdetailid = _context.DietDetails.OrderByDescending(p => p.DietDetailId).FirstOrDefault().DietDetailId;
            //建立圖片路徑
            var webRootPath = _webHostEnvironment.WebRootPath;
            var imageDirectory = Path.Combine(webRootPath, "DietImg");
            //若路徑中沒有資料夾則建立資料夾
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }         
            if (Img != null && Img.Count > 0)
            {
                foreach (var Image in Img)
                {

                    //存取資料
                    string fileName = $"{dietdetailid}_{Guid.NewGuid()}{Path.GetExtension(Image.FileName)}";
                    string filePath = Path.Combine(imageDirectory, fileName);
                    saveimg(Image, filePath, fileName, dietDTO);
                    //using (var stream = new FileStream(filePath, FileMode.Create))
                    //{
                    //    Image.CopyTo(stream);
                    //}
                    //DietImg saveimg = new DietImg
                    //{
                    //    DietDetailId = dietdetailid,
                    //    Img = fileName,
                    //};
                    //_context.DietImgs.Add(saveimg);

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
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePlan(int? id)
        //{
        //    DeletePlanDTO deleteitems = new DeletePlanDTO();
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    var plan = await _context.Plans.FindAsync(id);
        //    //移除plan的關聯資料
        //    deleteitems.diet = _context.Diets.Where(d => d.PlanId == id);
        //    deleteitems.sport = _context.Sports.Where(d => d.PlanId == id);
        //    deleteitems.water = _context.Water.Where(d => d.PlanId == id);
        //    deleteMethod(deleteitems);

        //    if (plan == null)
        //    {
        //        return NotFound();
        //    }
        //    _context.Plans.Remove(plan);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        // DELETE: api/Plans/5
        [Route("~/api/[controller]/sportdetail/delete/{detailId:int}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSportDetail(int? detailId)
        {
            if (detailId == null)
            {
                return NotFound();
            }
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
        private void saveimg(IFormFile img, string filePath, string fileName, DietDetailDTO dietDTO)
        {
            //設定亂數名稱存取資料
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                img.CopyTo(stream);
            }
            DietImg saveimg = new()
            {
                DietDetailId = dietDTO.DietDetailId,
                Img = fileName,
            };
            _context.DietImgs.Add(saveimg);
        }

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



        private DietDetail DietAdd(DietDetailDTO dto)
        {
            DietDetail diet = new()
            {
                DietDetailId = dto.DietDetailId,
                DietId = dto.DietId,
                Dname = dto.Dname,
                Type = dto.Type,
                Decription = dto.Decription,
                Calories = dto.Calories,
                Registerdate = Convert.ToDateTime(dto.Registerdate)
            };
            return diet;
        }

        private Plan PlanAdd(PlanDTO plan)
        {
            Plan res = new()
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
            };
            return res;
        }
        private SportDetail SportAdd(SportDetailDTO dto)
        {
            SportDetail sport = new()
            {
                SportDetailId = dto.SportDetailId,
                SportId = dto.SportId,
                Frequency = dto.Frequency,
                Isdone = false,
                Sname = dto.Sname,
                Registerdate = DateTime.Now,
                Time = dto.Time,
                Hour = dto.Hour,
                Min = dto.Min,
                Type = dto.Type,
                Sets = dto.Sets,
                Calories = dto.Calories,
            };
            return sport;
        }
        private bool PlanExists(int? id)
        {
            return (_context.Plans?.Any(e => e.PlanId == id)).GetValueOrDefault();
        }

        #endregion


    }
}
