using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POS.Models;

namespace POS.Controllers
{
    public class DailySalesEntriesController : Controller
    {
        private readonly POSDbContext _context;

        public DailySalesEntriesController(POSDbContext context)
        {
            _context = context;    
        }

        // GET: DailySalesEntries
        public async Task<IActionResult> Index(int? id, bool? error)
        {
            var inventoryControlDBContext = _context.DailySalesEntry
                .Where(s => s.DailySalesId == id);

            var paymentMethodQuery = from e in _context.PaymentMethod
                                     orderby e.ID.ToString()
                                     select e;

            ViewBag.PaymentMethod = new SelectList(paymentMethodQuery, "ID", "Type");

            var itemQuery = from i in _context.InventoryItem
                            orderby i.ID.ToString()
                            select i;

            ViewBag.InventoryItem = new SelectList(itemQuery, "ID", "Name");

            var totalDailySales = _context.DailySalesEntry
                .Where(s => s.DailySalesId == id);

            var totalCOPSales = totalDailySales
                .ToList()
                .Select(z => z.AmountCOP)
                .Sum();

            var totalUSDSales = totalDailySales
                .Where(s => s.PaymentMethodId == 2)
                .ToList()
                .Select(z => z.AmountUSD)
                .Sum();

            var salesUSDInCOP = totalDailySales
                .Where(s => s.PaymentMethodId == 2)
                .ToList()
                .Select(z => z.AmountCOP)
                .Sum();

            var totalTCSales = totalDailySales
                .Where(t => t.PaymentMethodId != 1 && t.PaymentMethodId != 2)
                .ToList()
                .Select(z => z.AmountCOP)
                .Sum();

            var cashBalanceCOP = totalCOPSales - salesUSDInCOP - totalTCSales;


            ViewData["TotalCOPSales"] = totalCOPSales.ToString("C");
            ViewData["TotalUSDSales"] = totalUSDSales.ToString("C");
            ViewData["TotalTCSales"] = totalTCSales.ToString("C");
            ViewData["CashBalanceCOP"] = cashBalanceCOP.ToString("C");

            return View(await inventoryControlDBContext.ToListAsync());
        }

        // GET: DailySalesEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailySalesEntry = await _context.DailySalesEntry
                .Include(d => d.DailySales)
                .Include(d => d.InventoryItem)
                .Include(d => d.PaymentMethod)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (dailySalesEntry == null)
            {
                return NotFound();
            }

            return View(dailySalesEntry);
        }

        // POST: DailySalesEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,InventoryItemId,DailySalesId,PaymentMethodId,Quantity")] DailySalesEntry dailySalesEntry)
        {
            if (ModelState.IsValid)
            {
                var itemDetails = _context.InventoryItem
                .Where(x => x.ID == dailySalesEntry.InventoryItemId)
                .First();

                var currentStockQty = itemDetails.StockQty;

                if(dailySalesEntry.Quantity > currentStockQty)
                {
                    //Add error message and add to span element
                    return RedirectToAction("Index", new {id = dailySalesEntry.DailySalesId});
                }

                itemDetails.StockQty -= dailySalesEntry.Quantity; 

                dailySalesEntry.ItemPriceCOP = itemDetails.PriceCOP;
                dailySalesEntry.AmountCOP = dailySalesEntry.Quantity * itemDetails.PriceCOP;

                dailySalesEntry.ItemPriceUSD = itemDetails.PriceUSD;
                dailySalesEntry.AmountUSD = dailySalesEntry.Quantity * itemDetails.PriceUSD;

                _context.Add(dailySalesEntry);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", new { id = dailySalesEntry.DailySalesId });
        }

        // GET: DailySalesEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailySalesEntry = await _context.DailySalesEntry.SingleOrDefaultAsync(m => m.ID == id);
            if (dailySalesEntry == null)
            {
                return NotFound();
            }
            ViewData["DailySalesId"] = new SelectList(_context.DailySalesModel, "ID", "ID", dailySalesEntry.DailySalesId);
            ViewData["InventoryItemId"] = new SelectList(_context.InventoryItem, "ID", "ID", dailySalesEntry.InventoryItemId);
            ViewData["PaymentMethodId"] = new SelectList(_context.Set<PaymentMethod>(), "ID", "ID", dailySalesEntry.PaymentMethodId);
            return View(dailySalesEntry);
        }

        // POST: DailySalesEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,InventoryItemId,DailySalesId,PaymentMethodId,Quantity")] DailySalesEntry dailySalesEntry)
        {
            if (id != dailySalesEntry.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dailySalesEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DailySalesEntryExists(dailySalesEntry.ID))
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
            ViewData["DailySalesId"] = new SelectList(_context.DailySalesModel, "ID", "ID", dailySalesEntry.DailySalesId);
            ViewData["InventoryItemId"] = new SelectList(_context.InventoryItem, "ID", "ID", dailySalesEntry.InventoryItemId);
            ViewData["PaymentMethodId"] = new SelectList(_context.Set<PaymentMethod>(), "ID", "ID", dailySalesEntry.PaymentMethodId);
            return View(dailySalesEntry);
        }

        // GET: DailySalesEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailySalesEntry = await _context.DailySalesEntry
                .Include(d => d.DailySales)
                .Include(d => d.InventoryItem)
                .Include(d => d.PaymentMethod)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (dailySalesEntry == null)
            {
                return NotFound();
            }

            return View(dailySalesEntry);
        }

        // POST: DailySalesEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dailySalesEntry = await _context.DailySalesEntry.SingleOrDefaultAsync(m => m.ID == id);
            _context.DailySalesEntry.Remove(dailySalesEntry);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { id = dailySalesEntry.DailySalesId });
        }

        private bool DailySalesEntryExists(int id)
        {
            return _context.DailySalesEntry.Any(e => e.ID == id);
        }
    }
}
