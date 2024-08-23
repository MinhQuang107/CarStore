using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarStore.Models;
using CarStore.Interfaces;
using CarStore.Services;
using System.Runtime.ConstrainedExecution;

namespace CarStore.Areas.Store.Controllers
{
    [Area("Store")]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: Store/Customers
        public async Task<IActionResult> Index()
        {
            return View(await _customerService.GetAllCustomersAsync());
        }

        // GET: Store/Customers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = await _customerService.GetCustomerByIdAsync(id.Value);
            if (customers == null)
            {
                return NotFound();
            }

            return View(customers);
        }

        // GET: Store/Customers/Create
        public IActionResult Create()
        {
            //ViewData["User_ID"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: Store/Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Email,Phone,Address,Referral_Status,Discount_Level,User_ID")] Customers customers)
		{
			if (ModelState.IsValid)
            {
				var result = await _customerService.AddCustomerAsync(customers);
				if (result)
				{
					return RedirectToAction(nameof(Index));
				}
				else
				{
					return View(customers);
				}
			}
            //ViewData["User_ID"] = new SelectList(_context.AspNetUsers, "Id", "Id", customers.User_ID);
            return View(customers);
        }

        // GET: Store/Customers/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
			if (id == null)
			{
				return NotFound();
			}
			else
			{
				var car = await _customerService.GetCustomerByIdAsync(id);
				return View(car);
			}
		}

        // POST: Store/Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ID,Name,Email,Phone,Address,Referral_Status,Discount_Level,User_ID")] Customers customers)
        {
			if (ModelState.IsValid)
			{
				try
				{
					var res = await _customerService.UpdateCustomerAsync(id, customers);
					if (res)
					{
						return RedirectToAction(nameof(Index));
					}
					else
					{
						return View(customers);
					}
				}
				catch (DbUpdateConcurrencyException)
				{
					return NotFound();
				}
			}
			return View(customers);
		}

        // GET: Store/Customers/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

			var customer = await _customerService.GetCustomerByIdAsync(id.Value);
			if (customer == null)
			{
				return NotFound();
			}
			return View(customer);
		}

        // POST: Store/Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
			try
			{
				var res = await _customerService.DeleteCustomerAsync(id);
				if (res)
				{
					return RedirectToAction(nameof(Index));
				}
				else
				{
					return Problem("This car maybe deleted before!");
				}
			}
			catch (Exception ex)
			{
				return NoContent();
			}
		}
    }
}
