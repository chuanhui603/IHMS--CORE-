using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IHMS.Models;
using IHMS.ViewModel;
using System.Diagnostics;

namespace IHMS.Controllers
{
    public class PointRecordsController : Controller
    {
        private readonly IhmsContext _context;

        public PointRecordsController(IhmsContext context)
        {
            _context = context;
        }

        // GET: PointRecords
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate, int page = 1, int pageSize = 20)
        {
            var ihms0702Context = _context.PointRecords.Include(p => p.Member);

            if(startDate != null && endDate !=null)
            {
                var datas = ihms0702Context.Where(p => p.Createtime >= startDate.Value && p.Createtime <= endDate.Value);

                int totalItem = datas.Count();
                int totalPage = (int)Math.Ceiling((double)totalItem / pageSize);
                var Pagedatas = datas.Skip((page - 1) * pageSize)
                     .Take(pageSize);

                ViewBag.TotalItems = totalItem;
                ViewBag.TotalPages = totalPage;
                ViewBag.CurrentPage = page;
                ViewBag.PageSize = pageSize;
                


                return View(Pagedatas);
            }

            int totalItems = ihms0702Context.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var data = ihms0702Context.OrderByDescending(p => p.Createtime).ThenByDescending(p => p.Count)
                               .Skip((page - 1) * pageSize)
                               .Take(pageSize);

            ViewBag.TotalItems = totalItems;
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            

            return View(await data.ToListAsync());
        }

        // GET: PointRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PointRecords == null)
            {
                return NotFound();
            }

            var pointRecord = await _context.PointRecords
                .Include(p => p.Member)
                .FirstOrDefaultAsync(m => m.PointrecordId == id);
            if (pointRecord == null)
            {
                return NotFound();
            }

            return View(pointRecord);
        }

       
        // GET: PointRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PointRecords == null)
            {
                return NotFound();
            }

            var pointRecord = await _context.PointRecords.FindAsync(id);

            if (pointRecord == null)
            {
                return NotFound();
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", pointRecord.MemberId);
            return View(pointRecord);
        }

        // POST: PointRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PointrecordId,Count,MemberId,BankNumber,Createtime,Updatetime")] PointRecord pointRecord)
        {
            if (id != pointRecord.PointrecordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pointRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PointRecordExists(pointRecord.PointrecordId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", pointRecord.MemberId);
            return View(pointRecord);
        }

        
        private bool PointRecordExists(int id)
        {
          return (_context.PointRecords?.Any(e => e.PointrecordId == id)).GetValueOrDefault();
        }
    }
}
