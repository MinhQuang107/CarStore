using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarStore.Models;

namespace CarStore.Areas.Store.Controllers
{
    [Area("Store")]
    public class Size_WeightController : Controller
    {
        private readonly CarStoreContext _context;

        public Size_WeightController(CarStoreContext context)
        {
            _context = context;
        }

        // GET: Store/Size_Weight
        public async Task<IActionResult> Index()
        {
            var carStoreContext = _context.Size_Weight.Include(s => s.Car);
            return View(await carStoreContext.ToListAsync());
        }

        // GET: Store/Size_Weight/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var size_Weight = await _context.Size_Weight
                .Include(s => s.Car)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (size_Weight == null)
            {
                return NotFound();
            }

            return View(size_Weight);
        }

        // GET: Store/Size_Weight/Create
        public IActionResult Create()
        {
            ViewData["Car_ID"] = new SelectList(_context.Car, "ID", "ID");
            return View();
        }

        // POST: Store/Size_Weight/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Car_ID,Number_of_seats,Length_mm,Width_mm,Height_mm,Wheelbase_mm,Curb_weight_kg,Gross_weight_kg")] Size_Weight size_Weight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(size_Weight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Car_ID"] = new SelectList(_context.Car, "ID", "ID", size_Weight.Car_ID);
            return View(size_Weight);
        }

        // GET: Store/Size_Weight/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var size_Weight = await _context.Size_Weight.FindAsync(id);
            if (size_Weight == null)
            {
                return NotFound();
            }
            ViewData["Car_ID"] = new SelectList(_context.Car, "ID", "ID", size_Weight.Car_ID);
            return View(size_Weight);
        }

        // POST: Store/Size_Weight/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Car_ID,Number_of_seats,Length_mm,Width_mm,Height_mm,Wheelbase_mm,Curb_weight_kg,Gross_weight_kg")] Size_Weight size_Weight)
        {
            if (id != size_Weight.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(size_Weight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Size_WeightExists(size_Weight.ID))
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
            ViewData["Car_ID"] = new SelectList(_context.Car, "ID", "ID", size_Weight.Car_ID);
            return View(size_Weight);
        }

        // GET: Store/Size_Weight/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var size_Weight = await _context.Size_Weight
                .Include(s => s.Car)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (size_Weight == null)
            {
                return NotFound();
            }

            return View(size_Weight);
        }

        // POST: Store/Size_Weight/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var size_Weight = await _context.Size_Weight.FindAsync(id);
            if (size_Weight != null)
            {
                _context.Size_Weight.Remove(size_Weight);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Size_WeightExists(int id)
        {
            return _context.Size_Weight.Any(e => e.ID == id);
        }
    }
}
