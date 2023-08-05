using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IHMS.Models;
using IHMS.ViewModel;

namespace IHMS.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IhmsContext _db;

        public OrdersController(IhmsContext db)
        {
            _db = db;
        }

        // GET: Orders
        public async Task<IActionResult> List(CKeywordViewModel vm, int? page)
        {
            string keyword = vm.txtKeyword;
            var ihms0702Context = _db.Orders.Include(o => o.Member);

            int pageSize = 10;
            int pageNumber = page ?? 1;



            if (!string.IsNullOrEmpty(keyword))
            {

                var datas = ihms0702Context.Where(p => p.Member.Name.Contains(keyword) ||
                                                p.Ordernumber.Contains(keyword) ||
                                                p.Pointstotal.ToString().Contains(keyword) ||
                                                p.State.Contains(keyword) ||
                                                p.Reason.Contains(keyword) ||
                                                p.Createtime.ToString().Contains(keyword)).OrderByDescending(p => p.Updatetime).ThenByDescending(p => p.Pointstotal);


                //搜尋的總數量
                int totalItem = datas.Count();
                //搜尋的總頁數= 數量/一頁的項次
                int totalPage = (int)Math.Ceiling((double)totalItem / pageSize);

                //分頁的總資料
                var Pagedatas = datas.Skip((pageNumber - 1) * pageSize)
                     .Take(pageSize);

                ViewBag.TotalItems = totalItem;
                ViewBag.TotalPages = totalPage;
                ViewBag.Keyword = keyword;  // 傳遞 keyword 到視圖           



                return View(Pagedatas);
            }

            int totalItems = ihms0702Context.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var data = ihms0702Context.OrderBy(p => p.Createtime)
                               .Skip((pageNumber - 1) * pageSize)
                               .Take(pageSize);

            ViewBag.TotalItems = totalItems;
            ViewBag.TotalPages = totalPages;
            //ViewBag.CurrentPage = page;
            //ViewBag.PageSize = pageSize;

            return View(data);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _db.Orders == null)
            {
                return NotFound();
            }

            var order = await _db.Orders
                .Include(o => o.Member)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }


        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _db.Orders == null)
            {
                return NotFound();
            }

            var member = await _db.Members.ToListAsync();

            var order = await _db.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            ViewData["MemberId"] = new SelectList(_db.Members, "MemberId", "MemberId", order.MemberId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            //ModelState.Remove("Member.Email");
            //ModelState.Remove("Member.Account");
            //ModelState.Remove("Member.Passeord");


            if (ModelState.IsValid)
            {
                try
                {
                    if (order.State == "已取消" && string.IsNullOrEmpty(order.Reason))
                    {
                        ModelState.AddModelError("Reason", "當選擇已取消時，請填寫原因");
                        return View(order);
                    }


                    _db.Update(order);
                    await _db.SaveChangesAsync();
                    //return RedirectToAction(nameof(List));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(List));
            }




            ViewData["MemberId"] = new SelectList(_db.Members, "MemberId", "MemberId", order.MemberId);
            return View(order);
        }


        private bool OrderExists(int id)
        {
            return (_db.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
