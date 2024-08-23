using CarStore.Interfaces;
using CarStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarStore.Services
{
    public class CarService : ICarService
    {
        private readonly CarStoreContext _context;

        public CarService(CarStoreContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCarAsync(Car car)
        {
            try
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và ghi nhật ký tại đây nếu cần thiết
                return false;
            }
        }

        public async Task<bool> DeleteCarAsync(long id)
        {
            try
            {
                var car = await _context.Car.Include(c => c.ImageCar)
                                .Include(c => c.Interior_Convenience)
                                .Include(c => c.Engine_Transmission)
                                .Include(c => c.Size_Weight)
                                .Include(c => c.Safety)
                                .Include(c => c.Exterior)
                                .FirstOrDefaultAsync(c => c.ID == id);
                if (car != null)
                {
                    _context.Remove(car);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và ghi nhật ký tại đây nếu cần thiết
                return false;
            }
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            try
            {
                var cars = await _context.Car
                    .Include(c => c.ImageCar)
                    .Include(c => c.Interior_Convenience)
                    .Include(c => c.Engine_Transmission)
                    .Include(c => c.Size_Weight)
                    .Include(c => c.Safety)
                    .Include(c => c.Exterior)
                    .ToListAsync();
                return cars;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và ghi nhật ký tại đây nếu cần thiết
                return null;
            }
        }

        public async Task<List<Car>> GetUsedCarsAsync()
        {
            try
            {
                var cars = await GetAllCarsAsync();
                var usedCars = cars.Where(c => !c.Car_Condition.Equals("New")).ToList();
                return usedCars;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và ghi nhật ký tại đây nếu cần thiết
                return null;
            }
        }

        public async Task<List<Car>> GetNewCarsAsync()
        {
            try
            {
                var cars = await GetAllCarsAsync();
                var newCars = cars.Where(c => c.Car_Condition.Equals("New")).ToList();
                return newCars;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và ghi nhật ký tại đây nếu cần thiết
                return null;
            }
        }

		public async Task<Car> GetCarByIdAsync(long? id)
        {
            try
            {
                return await _context.Car.FirstOrDefaultAsync(c => c.ID == id);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và ghi nhật ký tại đây nếu cần thiết
                return null;
            }
        }

        public async Task<bool> UpdateCarAsync(long id, Car updatedCar)
        {
            try
            {
                if (id == updatedCar.ID)
                {
                    _context.Car.Update(updatedCar);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và ghi nhật ký tại đây nếu cần thiết
                return false;
            }
        }

        public async Task<bool> AddCarImagesAsync(ImageCar imageCar)
        {
            try
            {
                if (!(await IsExistImageCarsAsync(imageCar)))
                {
                    _context.Add(imageCar);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và ghi nhật ký tại đây nếu cần thiết
                return false;
            }
        }

        public async Task<bool> UpdateCarImagesAsync(long id, ImageCar updatedImageCar)
        {
            try
            {
                if (id == updatedImageCar.Car_Id)
                {
                    _context.ImageCar.Update(updatedImageCar);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và ghi nhật ký tại đây nếu cần thiết
                return false;
            }
        }

        public async Task<bool> DeleteCarImagesAsync(long id)
        {
            try
            {
                var imgCar = await _context.ImageCar.FirstOrDefaultAsync(c => c.Id == id);
                if (imgCar != null)
                {
                    _context.Remove(imgCar);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và ghi nhật ký tại đây nếu cần thiết
                return false;
            }
        }

        public async Task<bool> AddCarImagesAsync(long? Car_ID, string? img_path)
        {
            try
            {
                ImageCar imageCar = new ImageCar();
                imageCar.Car_Id = (int)Car_ID;
                imageCar.Image_URL = img_path;
                return await AddCarImagesAsync(imageCar);
            } catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<bool> IsExistImageCarsAsync(ImageCar imageCar)
        {
            var res = await _context.ImageCar.AnyAsync(img => img.Car_Id == imageCar.Car_Id && img.Image_URL == imageCar.Image_URL);
            return res;
        }

        public async Task<List<Car>> GetCarsByOptionsAsync(string? condition, string? make, string? model, string? maxMileage, int? dateFrom, int? dateTo, string? priceFrom, string? priceTo, string? body)
        {
            try
            {
                var cars = await GetAllCarsAsync();
                var result = cars;
                if (condition != null)
                {
                    result = cars.Where(c => c.Car_Condition.Equals(condition)).ToList();
                }
                if (make != null)
                {
                    result = result.Where(c => c.Manufacturer.Equals(make)).ToList();
                }
                if (model != null)
                {
                    result = result.Where(c => c.Model.Equals((string)model)).ToList();
                }
                if (dateFrom != null || dateTo != null)
                {
                    if (dateFrom == null) dateFrom = DateTime.MinValue.Year;
                    if (dateTo == null) dateTo = DateTime.MaxValue.Year;
                    result = result.Where(c => (c.Year >= dateFrom && c.Year <= dateTo)).ToList();
                }
                if (priceFrom != null || priceTo != null)
                {
                    result = result.Where(c => (c.Price >= decimal.Parse(priceFrom) && c.Price <= decimal.Parse(priceTo))).ToList();
                }
                if (body != null)
                {
                    result = result.Where(c => c.Body.Equals(body)).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và ghi nhật ký tại đây nếu cần thiết
                return null;
            }
        }
    }
}
