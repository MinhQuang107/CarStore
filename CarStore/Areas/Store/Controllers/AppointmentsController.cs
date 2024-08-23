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
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ICustomerService _customerService;

        public AppointmentsController(IAppointmentService appointmentService, ICustomerService customerService)
        {
            _appointmentService = appointmentService;
            _customerService = customerService;
        }

        // GET: Store/Appointments
        public async Task<IActionResult> Index()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return View(appointments);
        }

        // GET: Store/Appointments/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _appointmentService.GetAppointmentByIdAsync(id.Value);

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Store/Appointments/Create
        public async Task<IActionResult> Create()
        {
			ViewData["Customer_ID"] = new SelectList(await _customerService.GetAllCustomersAsync(), "ID", "ID");
            return View();
        }

        // POST: Store/Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Appointment appointment)
        {
			if (ModelState.IsValid)
			{
				var result = await _appointmentService.AddAppointmentAsync(appointment);
				if (result)
				{
					return RedirectToAction(nameof(Index));
				}
				else
				{
					return View(appointment);
				}
			}
			//ViewData["Customer_ID"] = new SelectList(_context.Customers, "ID", "ID", appointment.Customer_ID);
   //         ViewData["Employee_ID"] = new SelectList(_context.Employees, "ID", "ID", appointment.Employee_ID);
            return View(appointment);
        }

        // GET: Store/Appointments/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
			if (id == null)
			{
				return NotFound();
			}
			else
			{
				var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
				return View(appointment);
			}
			//ViewData["Customer_ID"] = new SelectList(_context.Customers, "ID", "ID", appointment.Customer_ID);
   //         ViewData["Employee_ID"] = new SelectList(_context.Employees, "ID", "ID", appointment.Employee_ID);
            //return View(appointment);
        }

        // POST: Store/Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ID,Employee_ID,Customer_ID,Car_Type,Car_Model,Purpose,Budget,Status,Location,Note,Reminder,Feedback,Follow_up_Appointment")] Appointment appointment)
        {
			if (ModelState.IsValid)
			{
				var result = await _appointmentService.AddAppointmentAsync(appointment);
				if (result)
				{
					return RedirectToAction(nameof(Index));
				}
				else
				{
					return View(appointment);
				}
			}
			//ViewData["User_ID"] = new SelectList(_context.AspNetUsers, "Id", "Id", customers.User_ID);
			//ViewData["Customer_ID"] = new SelectList(_context.Customers, "ID", "ID", appointment.Customer_ID);
            //ViewData["Employee_ID"] = new SelectList(_context.Employees, "ID", "ID", appointment.Employee_ID);
            return View(appointment);
        }

        // GET: Store/Appointments/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
			if (id == null)
			{
				return NotFound();
			}

			var appointment = await _appointmentService.GetAppointmentByIdAsync(id.Value);
			if (appointment == null)
			{
				return NotFound();
			}
			return View(appointment);
		}

        // POST: Store/Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
			try
			{
				var res = await _appointmentService.DeleteAppointmentAsync(id);
				if (res)
				{
					return RedirectToAction(nameof(Index));
				}
				else
				{
					return Problem("This appointment maybe deleted before!");
				}
			}
			catch (Exception ex)
			{
				return NoContent();
			}
		}
    }
}
