using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarStore.Models;

namespace CarStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountantsController : Controller
    {
        private readonly CarStoreContext _context;

        public AccountantsController(CarStoreContext context)
        {
            _context = context;
        }

        // GET: Admin/Accountants
        public async Task<IActionResult> Index()
        {
            var carStoreContext = _context.Accountants.Include(a => a.IDNavigation);
            return View(await carStoreContext.ToListAsync());
        }

        // GET: Admin/Accountants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountants = await _context.Accountants
                .Include(a => a.IDNavigation)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (accountants == null)
            {
                return NotFound();
            }

            return View(accountants);
        }

        // GET: Admin/Accountants/Create
        public IActionResult Create(int? eID)
        {
            ViewData["eID"] = eID;
            //ViewData["ID"] = new SelectList(_context.Employees, "ID", "ID");
            return View();
        }

        // POST: Admin/Accountants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Salary,Responsibilities,Qualifications")] Accountants accountants)
        {
            if (ModelState.IsValid)
            {
				accountants.IDNavigation = await _context.Employees.FirstOrDefaultAsync(e => e.ID == accountants.ID);
				_context.Add(accountants);
                await _context.SaveChangesAsync();
                //return View(accountants);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ID"] = new SelectList(_context.Employees, "ID", "ID", accountants.ID);
            return View(accountants);
        }

        // GET: Admin/Accountants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountants = await _context.Accountants.FindAsync(id);
            if (accountants == null)
            {
                return NotFound();
            }
            ViewData["ID"] = new SelectList(_context.Employees, "ID", "ID", accountants.ID);
            return View(accountants);
        }

        // POST: Admin/Accountants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Salary,Responsibilities,Qualifications")] Accountants accountants)
        {
            if (id != accountants.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountants);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountantsExists(accountants.ID))
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
            ViewData["ID"] = new SelectList(_context.Employees, "ID", "ID", accountants.ID);
            return View(accountants);
        }

        // GET: Admin/Accountants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountants = await _context.Accountants
                .Include(a => a.IDNavigation)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (accountants == null)
            {
                return NotFound();
            }

            return View(accountants);
        }

        // POST: Admin/Accountants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accountants = await _context.Accountants.FindAsync(id);
            if (accountants != null)
            {
                _context.Accountants.Remove(accountants);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountantsExists(int id)
        {
            return _context.Accountants.Any(e => e.ID == id);
        }
    }
}
