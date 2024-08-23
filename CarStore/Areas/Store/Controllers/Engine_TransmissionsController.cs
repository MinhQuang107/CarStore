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
    public class Engine_TransmissionsController : Controller
    {
        private readonly CarStoreContext _context;

        public Engine_TransmissionsController(CarStoreContext context)
        {
            _context = context;
        }

        // GET: Store/Engine_Transmissions
        public async Task<IActionResult> Index()
        {
            var carStoreContext = _context.Engine_Transmission.Include(e => e.Car);
            return View(await carStoreContext.ToListAsync());
        }

        // GET: Store/Engine_Transmissions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var engine_Transmission = await _context.Engine_Transmission
                .Include(e => e.Car)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (engine_Transmission == null)
            {
                return NotFound();
            }

            return View(engine_Transmission);
        }

        // GET: Store/Engine_Transmissions/Create
        public IActionResult Create()
        {
            ViewData["Car_ID"] = new SelectList(_context.Car, "ID", "ID");
            return View();
        }

        // POST: Store/Engine_Transmissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Car_ID,Engine_type,Maximum_power,Maximum_torque,Transmission_type,Drivetrain,Acceleration_0_100kmh,Maximum_speed_kmh,Fuel_type,ECO_start_stop")] Engine_Transmission engine_Transmission)
        {
            if (ModelState.IsValid)
            {
                _context.Add(engine_Transmission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Car_ID"] = new SelectList(_context.Car, "ID", "ID", engine_Transmission.Car_ID);
            return View(engine_Transmission);
        }

        // GET: Store/Engine_Transmissions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var engine_Transmission = await _context.Engine_Transmission.FindAsync(id);
            if (engine_Transmission == null)
            {
                return NotFound();
            }
            ViewData["Car_ID"] = new SelectList(_context.Car, "ID", "ID", engine_Transmission.Car_ID);
            return View(engine_Transmission);
        }

        // POST: Store/Engine_Transmissions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Car_ID,Engine_type,Maximum_power,Maximum_torque,Transmission_type,Drivetrain,Acceleration_0_100kmh,Maximum_speed_kmh,Fuel_type,ECO_start_stop")] Engine_Transmission engine_Transmission)
        {
            if (id != engine_Transmission.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(engine_Transmission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Engine_TransmissionExists(engine_Transmission.ID))
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
            ViewData["Car_ID"] = new SelectList(_context.Car, "ID", "ID", engine_Transmission.Car_ID);
            return View(engine_Transmission);
        }

        // GET: Store/Engine_Transmissions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var engine_Transmission = await _context.Engine_Transmission
                .Include(e => e.Car)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (engine_Transmission == null)
            {
                return NotFound();
            }

            return View(engine_Transmission);
        }

        // POST: Store/Engine_Transmissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var engine_Transmission = await _context.Engine_Transmission.FindAsync(id);
            if (engine_Transmission != null)
            {
                _context.Engine_Transmission.Remove(engine_Transmission);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Engine_TransmissionExists(int id)
        {
            return _context.Engine_Transmission.Any(e => e.ID == id);
        }
    }
}
