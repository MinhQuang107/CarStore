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
    public class ExteriorsController : Controller
    {
        private readonly CarStoreContext _context;

        public ExteriorsController(CarStoreContext context)
        {
            _context = context;
        }

        // GET: Store/Exteriors
        public async Task<IActionResult> Index()
        {
            var carStoreContext = _context.Exterior.Include(e => e.Car);
            return View(await carStoreContext.ToListAsync());
        }

        // GET: Store/Exteriors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exterior = await _context.Exterior
                .Include(e => e.Car)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (exterior == null)
            {
                return NotFound();
            }

            return View(exterior);
        }

        // GET: Store/Exteriors/Create
        public IActionResult Create()
        {
            ViewData["Car_ID"] = new SelectList(_context.Car, "ID", "ID");
            return View();
        }

        // POST: Store/Exteriors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Car_ID,High_beam,Low_beam,Daytime_running_lights,Tail_lights,Rearview_mirrors,Rain_sensing_wipers,Power_trunk_lid,Hands_free_trunk_opening")] Exterior exterior)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exterior);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Car_ID"] = new SelectList(_context.Car, "ID", "ID", exterior.Car_ID);
            return View(exterior);
        }

        // GET: Store/Exteriors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exterior = await _context.Exterior.FindAsync(id);
            if (exterior == null)
            {
                return NotFound();
            }
            ViewData["Car_ID"] = new SelectList(_context.Car, "ID", "ID", exterior.Car_ID);
            return View(exterior);
        }

        // POST: Store/Exteriors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Car_ID,High_beam,Low_beam,Daytime_running_lights,Tail_lights,Rearview_mirrors,Rain_sensing_wipers,Power_trunk_lid,Hands_free_trunk_opening")] Exterior exterior)
        {
            if (id != exterior.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exterior);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExteriorExists(exterior.ID))
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
            ViewData["Car_ID"] = new SelectList(_context.Car, "ID", "ID", exterior.Car_ID);
            return View(exterior);
        }

        // GET: Store/Exteriors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exterior = await _context.Exterior
                .Include(e => e.Car)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (exterior == null)
            {
                return NotFound();
            }

            return View(exterior);
        }

        // POST: Store/Exteriors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exterior = await _context.Exterior.FindAsync(id);
            if (exterior != null)
            {
                _context.Exterior.Remove(exterior);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExteriorExists(int id)
        {
            return _context.Exterior.Any(e => e.ID == id);
        }
    }
}
