using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace POS.Controllers
{
    public class AddInventoryController : Controller
    {
        private readonly POSDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AddInventoryController(POSDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: AddInventory
        public async Task<IActionResult> Index()
        {
            return View(await _context.AddInventoryModel.ToListAsync());
        }

        // GET: AddInventory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addInventoryModel = await _context.AddInventoryModel
                .SingleOrDefaultAsync(m => m.ID == id);
            if (addInventoryModel == null)
            {
                return NotFound();
            }

            return View(addInventoryModel);
        }

        // GET: AddInventory/Create
        public async Task<IActionResult> Create()
        {
            var inventoryItems = await _context.InventoryItem.ToListAsync();
            List<AddInventoryViewModel> addInventoryViewModels = new List<AddInventoryViewModel>();


            foreach(var item in inventoryItems)
            {
                var addInventoryViewModel = new AddInventoryViewModel();
                addInventoryViewModel.ID = item.ID;
                addInventoryViewModel.Name = item.Name;
                addInventoryViewModel.Quantity = 0;
                addInventoryViewModels.Add(addInventoryViewModel);
            }
            return View(addInventoryViewModels);
        }

        // POST: AddInventory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection formCollection)
        {
            
            if (ModelState.IsValid)
            {
                //Save new AddInventoryModel
                AddInventoryModel addInventoryModel = new AddInventoryModel();
                addInventoryModel.Date = DateTime.Now;
                addInventoryModel.UserId = _userManager.get
                _context.Add(addInventoryModel);
                await _context.SaveChangesAsync();

                //Save new AddedItems to AddInventoryModel and update DB with new quantities
                var inventory = _context.InventoryItem;

                foreach (var _key in formCollection.Keys)
                {
                    if (_key != "__RequestVerificationToken")
                    {
                        var item = inventory.Find(Convert.ToInt32(_key));
                        int addQty = Convert.ToInt32(formCollection[_key]);

                        //Add addedItem to _context and populate AddedItems
                        AddedItem addedItem = new AddedItem();
                        addedItem.AddInventoryModelId = addInventoryModel.ID;
                        addedItem.InventoryItemId = item.ID;
                        addedItem.Quantity = addQty;
                        _context.Add(addedItem);

                        //Update InventoryItem new StockQty
                        item.StockQty += addQty;
                        
                    }

                }

                await _context.SaveChangesAsync();

                return Redirect("../InventoryItems");
            }

            return RedirectToAction("AddInventory/Create");
        }

        // GET: AddInventory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addInventoryModel = await _context.AddInventoryModel.SingleOrDefaultAsync(m => m.ID == id);
            if (addInventoryModel == null)
            {
                return NotFound();
            }
            return View(addInventoryModel);
        }

        // POST: AddInventory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Date,Quantity")] AddInventoryModel addInventoryModel)
        {
            if (id != addInventoryModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(addInventoryModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddInventoryModelExists(addInventoryModel.ID))
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
            return View(addInventoryModel);
        }

        // GET: AddInventory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addInventoryModel = await _context.AddInventoryModel
                .SingleOrDefaultAsync(m => m.ID == id);
            if (addInventoryModel == null)
            {
                return NotFound();
            }

            return View(addInventoryModel);
        }

        // POST: AddInventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var addInventoryModel = await _context.AddInventoryModel.SingleOrDefaultAsync(m => m.ID == id);
            _context.AddInventoryModel.Remove(addInventoryModel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AddInventoryModelExists(int id)
        {
            return _context.AddInventoryModel.Any(e => e.ID == id);
        }
    }
}
