using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POS.Models;
using POS.Data;

    namespace POS.Controllers
{
    public class DailySalesModelsController : Controller
    {
        private readonly POSDbContext _context;
        private readonly ApplicationDbContext _applicationContext;

        public DailySalesModelsController(POSDbContext context, ApplicationDbContext applicationContext)
        {
            _context = context;
            _applicationContext = applicationContext;
        }

        // GET: DailySalesModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.DailySalesModel.ToListAsync());
        }

        // GET: DailySalesModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailySalesModel = await _context.DailySalesModel
                .SingleOrDefaultAsync(m => m.ID == id);
            if (dailySalesModel == null)
            {
                return NotFound();
            }

            return View(dailySalesModel);
        }

        // GET: DailySalesModels/Create
        public IActionResult Create()
        {
            var salesRepQuery = from e in _applicationContext.Users
                              orderby e.Name
                              select e;

            ViewBag.SalesRepId = new SelectList(salesRepQuery, "Name", "Name");

            return View();
        }

        // POST: DailySalesModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SalesRep,Date")] DailySalesModel dailySalesModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dailySalesModel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(dailySalesModel);
        }

        // GET: DailySalesModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailySalesModel = await _context.DailySalesModel.SingleOrDefaultAsync(m => m.ID == id);
            if (dailySalesModel == null)
            {
                return NotFound();
            }
            return View(dailySalesModel);
        }

        // POST: DailySalesModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SalesRepId,Date")] DailySalesModel dailySalesModel)
        {
            if (id != dailySalesModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dailySalesModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DailySalesModelExists(dailySalesModel.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(dailySalesModel);
        }

        // GET: DailySalesModels/Delete/5
        public async Task<IActionResult> Delete(int? id)    
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailySalesModel = await _context.DailySalesModel
                .SingleOrDefaultAsync(m => m.ID == id);
            if (dailySalesModel == null)
            {
                return NotFound();
            }

            return View(dailySalesModel);
        }

        // POST: DailySalesModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dailySalesModel = await _context.DailySalesModel.SingleOrDefaultAsync(m => m.ID == id);
            _context.DailySalesModel.Remove(dailySalesModel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DailySalesModelExists(int id)
        {
            return _context.DailySalesModel.Any(e => e.ID == id);
        }
    }
}
