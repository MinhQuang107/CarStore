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
    public class SafetiesController : Controller
    {
        private readonly CarStoreContext _context;

        public SafetiesController(CarStoreContext context)
        {
            _context = context;
        }

        // GET: Store/Safeties
        public async Task<IActionResult> Index()
        {
            var carStoreContext = _context.Safety.Include(s => s.Car);
            return View(await carStoreContext.ToListAsync());
        }

        // GET: Store/Safeties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var safety = await _context.Safety
                .Include(s => s.Car)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (safety == null)
            {
                return NotFound();
            }

            return View(safety);
        }

        // GET: Store/Safeties/Create
        public IActionResult Create()
        {
            ViewData["Car_ID"] = new SelectList(_context.Car, "ID", "ID");
            return View();
        }

        // POST: Store/Safeties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Car_ID,Airbags,ABS_BAS_brakes,Electronic_stability_control,Vehicle_body_stability,Electronic_traction_control,Traction_control_during_acceleration,Crosswind_stability,Hill_start_assist,Hill_descent_assist,Active_parking_assistance,Automatic_protection,Attention_assist,Opt_360_degree_camera")] Safety safety)
        {
            if (ModelState.IsValid)
            {
                _context.Add(safety);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Car_ID"] = new SelectList(_context.Car, "ID", "ID", safety.Car_ID);
            return View(safety);
        }

        // GET: Store/Safeties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var safety = await _context.Safety.FindAsync(id);
            if (safety == null)
            {
                return NotFound();
            }
            ViewData["Car_ID"] = new SelectList(_context.Car, "ID", "ID", safety.Car_ID);
            return View(safety);
        }

        // POST: Store/Safeties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Car_ID,Airbags,ABS_BAS_brakes,Electronic_stability_control,Vehicle_body_stability,Electronic_traction_control,Traction_control_during_acceleration,Crosswind_stability,Hill_start_assist,Hill_descent_assist,Active_parking_assistance,Automatic_protection,Attention_assist,Opt_360_degree_camera")] Safety safety)
        {
            if (id != safety.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(safety);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SafetyExists(safety.ID))
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
            ViewData["Car_ID"] = new SelectList(_context.Car, "ID", "ID", safety.Car_ID);
            return View(safety);
        }

        // GET: Store/Safeties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var safety = await _context.Safety
                .Include(s => s.Car)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (safety == null)
            {
                return NotFound();
            }

            return View(safety);
        }

        // POST: Store/Safeties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var safety = await _context.Safety.FindAsync(id);
            if (safety != null)
            {
                _context.Safety.Remove(safety);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SafetyExists(int id)
        {
            return _context.Safety.Any(e => e.ID == id);
        }
    }
}
