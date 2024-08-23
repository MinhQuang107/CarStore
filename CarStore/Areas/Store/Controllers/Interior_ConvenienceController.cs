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
    public class Interior_ConvenienceController : Controller
    {
        private readonly CarStoreContext _context;

        public Interior_ConvenienceController(CarStoreContext context)
        {
            _context = context;
        }

        // GET: Store/Interior_Convenience
        public async Task<IActionResult> Index()
        {
            var carStoreContext = _context.Interior_Convenience.Include(i => i.Car);
            return View(await carStoreContext.ToListAsync());
        }

        // GET: Store/Interior_Convenience/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interior_Convenience = await _context.Interior_Convenience
                .Include(i => i.Car)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (interior_Convenience == null)
            {
                return NotFound();
            }

            return View(interior_Convenience);
        }

        // GET: Store/Interior_Convenience/Create
        public IActionResult Create()
        {
            ViewData["Car_ID"] = new SelectList(_context.Car, "ID", "ID");
            return View();
        }

        // POST: Store/Interior_Convenience/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Car_ID,Interior_material,Steering_wheel,Multi_information_display,Gear_shift_paddles,Keyless_entry_system,Cruise_Control,Electronic_parking_brake,Auto_dimming_rearview_mirror,Seats,Front_seats,Second_row_seats,Third_row_seats,Automatic_climate_control,Entertainment_screen,Audio_system,Touchpad,Voice_control,Integrated_GPS_navigation,Apple_Carplay_Android_Auto,Wireless_phone_charging,Starry_sky,Sunroof,Soft_close_doors")] Interior_Convenience interior_Convenience)
        {
            if (ModelState.IsValid)
            {
                _context.Add(interior_Convenience);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Car_ID"] = new SelectList(_context.Car, "ID", "ID", interior_Convenience.Car_ID);
            return View(interior_Convenience);
        }

        // GET: Store/Interior_Convenience/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interior_Convenience = await _context.Interior_Convenience.FindAsync(id);
            if (interior_Convenience == null)
            {
                return NotFound();
            }
            ViewData["Car_ID"] = new SelectList(_context.Car, "ID", "ID", interior_Convenience.Car_ID);
            return View(interior_Convenience);
        }

        // POST: Store/Interior_Convenience/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Car_ID,Interior_material,Steering_wheel,Multi_information_display,Gear_shift_paddles,Keyless_entry_system,Cruise_Control,Electronic_parking_brake,Auto_dimming_rearview_mirror,Seats,Front_seats,Second_row_seats,Third_row_seats,Automatic_climate_control,Entertainment_screen,Audio_system,Touchpad,Voice_control,Integrated_GPS_navigation,Apple_Carplay_Android_Auto,Wireless_phone_charging,Starry_sky,Sunroof,Soft_close_doors")] Interior_Convenience interior_Convenience)
        {
            if (id != interior_Convenience.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(interior_Convenience);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Interior_ConvenienceExists(interior_Convenience.ID))
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
            ViewData["Car_ID"] = new SelectList(_context.Car, "ID", "ID", interior_Convenience.Car_ID);
            return View(interior_Convenience);
        }

        // GET: Store/Interior_Convenience/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interior_Convenience = await _context.Interior_Convenience
                .Include(i => i.Car)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (interior_Convenience == null)
            {
                return NotFound();
            }

            return View(interior_Convenience);
        }

        // POST: Store/Interior_Convenience/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var interior_Convenience = await _context.Interior_Convenience.FindAsync(id);
            if (interior_Convenience != null)
            {
                _context.Interior_Convenience.Remove(interior_Convenience);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Interior_ConvenienceExists(int id)
        {
            return _context.Interior_Convenience.Any(e => e.ID == id);
        }
    }
}
