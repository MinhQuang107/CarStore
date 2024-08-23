using CarStore.Interfaces;
using CarStore.Models;
using CarStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using System.Text.Json.Serialization;
using System.Text.Json;
using CarStore.Areas.Store.Models;

namespace CarStore.Areas.Store.Controllers
{
    //[Authorize(Roles ="Admin")]
    [Area("Store")]
    public class CarsController : Controller
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }
        public async Task<IActionResult> Index()
        {
            var cars = await _carService.GetAllCarsAsync();
            return View(cars);
        }

        public async Task<IActionResult> IndexCondition(string condition = "Used")
        {
            var cars = await _carService.GetUsedCarsAsync();
            if (condition.Equals("New"))
            {
                cars = await _carService.GetNewCarsAsync();       
            }
            return View("Index", cars);
        }

        public async Task<IActionResult> QuickSearch()
        {
            var cars = await _carService.GetAllCarsAsync();
            ViewData["conditions"] = cars.Select(c => c.Car_Condition).ToList();
            ViewData["makes"] = cars.Select(c => c.Manufacturer).ToList();
            ViewData["models"] = cars.Select(c => c.Model).ToList();
            ViewData["body"] = cars.Select(c => c.Body).ToList();
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> SearchCar(string? condition, string? make, string? model, string? maxMileage, int? dateFrom, int? dateTo, string? priceFrom, string? priceTo, string? body)
		{
            var cars = await _carService.GetAllCarsAsync();
            ViewData["conditions"] = cars.Select(c => c.Car_Condition).ToList();
            ViewData["makes"] = cars.Select(c => c.Manufacturer).ToList();
            ViewData["models"] = cars.Select(c => c.Model).ToList();
            ViewData["body"] = cars.Select(c => c.Body).ToList();

            var carsResult = await _carService.GetCarsByOptionsAsync(condition, make, model, maxMileage, dateFrom, dateTo, priceFrom, priceTo, body);

			return View("QuickSearch", carsResult);
		}
        [HttpGet, HttpPost]
        public async Task<IActionResult> Search(string? condition, string? make, string? model, string? maxMileage, int? dateFrom, int? dateTo, string? priceFrom, string? priceTo, string? body, int pageIndex = 1)
        {
            int pageSize = 4; // Set the number of items per page

            var cars = await _carService.GetAllCarsAsync();
            ViewData["conditions"] = cars.Select(c => c.Car_Condition).Distinct().ToList();
            ViewData["makes"] = cars.Select(c => c.Manufacturer).Distinct().ToList();
            ViewData["models"] = cars.Select(c => c.Model).Distinct().ToList();
            ViewData["body"] = cars.Select(c => c.Body).Distinct().ToList();

            var carsResult = await _carService.GetCarsByOptionsAsync(condition, make, model, maxMileage, dateFrom, dateTo, priceFrom, priceTo, body);
            var paginatedCars = await PaginatedList<Car>.CreateAsync(carsResult, pageIndex, pageSize);

            return View("List", paginatedCars);
        }

        public IActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Car car)
        {
            if (ModelState.IsValid)
            {
                var result = await _carService.AddCarAsync(car);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(car);
                }
            }
            return View(car);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            } else
            {
                var car = await _carService.GetCarByIdAsync(id);
                return View(car);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Car car)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var res = await _carService.UpdateCarAsync(id, car);
                    if (res)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return View(car);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            }
            return View(car);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var car = await _carService.GetCarByIdAsync(id.Value);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var car = await _carService.GetCarByIdAsync(id.Value);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            try
            {
                var res = await _carService.DeleteCarAsync(id);
                if (res)
                {
                    return RedirectToAction(nameof(Index));
                } else
                {
                    return Problem("This car maybe deleted before!");
                }
            } catch (Exception ex)
            {
                return NoContent();
            }
        }
    }
}
