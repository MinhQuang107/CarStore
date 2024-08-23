using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarStore.Models;
using CarStore.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;

namespace CarStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeesController : Controller
    {
		private readonly UserManager<AppUser> _userManager;

		private readonly CarStoreContext _context;

        public EmployeesController(CarStoreContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Admin/Employees
        public async Task<IActionResult> Index()
        {
            var carStoreContext = _context.Employees
                                        .Include(e => e.User);
            return View(await carStoreContext.ToListAsync());
        }

        // GET: Admin/Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        // GET: Admin/Employees/Create
        public IActionResult Create()
        {
            //ViewData["User_ID"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            var listEmployeePosition = new List<string>
            {
                "Accountants",
                "Maintainance_Staff",
                "Salespeople",
                "Security",
            };
            ViewData["User_ID"] = new SelectList(_userManager.Users, "Id", "FullName");
            ViewData["ListPosition"] = listEmployeePosition;
            return View();
        }

        // POST: Admin/Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ID,Name,Email,Phone,Address,Position,User_ID")] Employees employees)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(employees);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["User_ID"] = new SelectList(_context.AspNetUsers, "Id", "Id", employees.User_ID);
        //    return View(employees);
        //}

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string? name, string? email,
                                        string? phone, string? address, string? position, string? user_id)
        {
            var employees = new Employees
            {
                Name = name,
                Email = email,
                Phone = phone,
                Address = address,
                Position = position,
                User_ID = user_id
            };
            var listEmployeePosition = new List<string>
            {
                    "Accountants",
                    "Maintainance_Staff",
                    "Salespeople",
                    "Security",
            };
            ViewData["ListPosition"] = listEmployeePosition;
            if (name != null && email != null && phone != null && position != null)
            {
                _context.Add(employees);
                await _context.SaveChangesAsync();
                //ViewData["User_ID"] = new SelectList(_userManager.Users, "Id", "FullName");
                var empAdded = await _context.Employees.FirstOrDefaultAsync(e => e.Name == name 
                                                                        && e.Email == email 
                                                                        && e.Phone == phone
                                                                        && e.Address == address
                                                                        && e.Position == position
                                                                        && e.User_ID == user_id);
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Create", "Accountants", new { eID = empAdded.ID });
            }
            ViewData["User_ID"] = new SelectList(_context.AspNetUsers, "Id", "Id", employees.User_ID);
            return View(employees);
        }

        // GET: Admin/Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees.FindAsync(id);
            if (employees == null)
            {
                return NotFound();
            }
            ViewData["User_ID"] = new SelectList(_context.AspNetUsers, "Id", "Id", employees.User_ID);
            return View(employees);
        }

        // POST: Admin/Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Email,Phone,Address,Position,User_ID")] Employees employees)
        {
            if (id != employees.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employees);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeesExists(employees.ID))
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
            ViewData["User_ID"] = new SelectList(_context.AspNetUsers, "Id", "Id", employees.User_ID);
            return View(employees);
        }

        // GET: Admin/Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        // POST: Admin/Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employees = await _context.Employees.FindAsync(id);
            if (employees != null)
            {
                _context.Employees.Remove(employees);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeesExists(int id)
        {
            return _context.Employees.Any(e => e.ID == id);
        }
    }
}
