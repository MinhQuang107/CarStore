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
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace CarStore.Areas.Store.Controllers
{
    [Area("Store")]

    public class ContactController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ICustomerService _customerService;

        public ContactController(IAppointmentService appointmentService, ICustomerService customerService)
        {
            _appointmentService = appointmentService;
            _customerService = customerService;
        }

        // GET: Store/Contact
        public async Task<IActionResult> Index()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return View(appointments);
        }

        // GET: Store/Contact/Details/5
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

        // GET: Store/Contact/Create
        public async Task<IActionResult> Create()
        {
            //ViewData["Customer_ID"] = new SelectList(await _customerService.GetAllCustomersAsync(), "ID", "ID");
            return View();
        }

        public List<string> isValidContact(string? name, string? email, string? phone, string? address, string? carType, string? carModel, string? purpose, string? budget, string? location, string? note, string? reminder,
                                                string? feedback, string? followUpAppointment, int status = 0)
        {
            List<string> errors = new List<string>();

            if (!string.IsNullOrEmpty(email) && !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errors.Add("Email không hợp lệ.");
            }

            if (!string.IsNullOrEmpty(phone) && !Regex.IsMatch(phone, @"^\+?\d{10,15}$"))
            {
                errors.Add("Số điện thoại không hợp lệ.");
            }

            return errors;
        }
        // POST: Store/Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string? name, string? email, string? phone, string? address, string? carType, string? carModel, string? purpose, string? budget, string? location, string? note, string? reminder, 
                                                string? feedback, string? followUpAppointment, int status = 0)
        {
            var errors = isValidContact(name, email, phone, address, carType, carModel, purpose,
                budget, location, note, reminder, feedback, followUpAppointment, status);
            ViewData["Errors"] = errors;

            if (errors.Count() == 0)
            {
                var customer = new Customers
                {
                    Name = name,
                    Email = email,
                    Phone = phone,
                    Address = address,
                    Referral_Status = "",
                    Discount_Level = 0,
                };
                long cusId = 0;
                try
                {
                    var cus = await _customerService.GetCustomerByOptions(name, email, phone, address);
                    if (cus == null)
                    {
                        var rs = await _customerService.AddCustomerAsync(customer);
                    }
                    cus = await _customerService.GetCustomerByOptions(name, email, phone, address);
                    if (cus != null) cusId = cus.ID;
                    var appointment = new Appointment
                    {
                        Customer_ID = (int)cusId,
                        Car_Type = carType,
                        Car_Model = carModel,
                        Purpose = purpose,
                        Budget = Convert.ToDecimal(budget),
                        Location = location,
                        Note = note,
                        Reminder = DateTime.Parse(reminder),
                        Feedback = feedback,
                        Follow_up_Appointment = followUpAppointment,
                        Status = status.ToString(),
                    };
                    var result = await _appointmentService.AddAppointmentAsync(appointment);
                    if (result)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    return NotFound();
                }
                
            } else
            {
                return View();
            }
            //ViewData["Customer_ID"] = new SelectList(_context.Customers, "ID", "ID", appointment.Customer_ID);
            //         ViewData["Employee_ID"] = new SelectList(_context.Employees, "ID", "ID", appointment.Employee_ID);
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
