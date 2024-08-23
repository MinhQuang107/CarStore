using CarStore.Interfaces;
using CarStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarStore.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CarStoreContext _context;

        public CustomerService(CarStoreContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCustomerAsync(Customers customer)
        {
            try
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và ghi nhật ký tại đây nếu cần thiết
                return false;
            }
        }

        public async Task<bool> DeleteCustomerAsync(long id)
        {
            try
            {
                var customer = await _context.Customers
				                    .Include(c => c.User)
				                    .FirstOrDefaultAsync(m => m.ID == id);
				if (customer != null)
                {
                    _context.Remove(customer);
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

        public async Task<List<Customers>> GetAllCustomersAsync()
        {
            try
            {
                var customers = await _context.Customers
				                        .Include(c => c.User).ToListAsync();
				return customers;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và ghi nhật ký tại đây nếu cần thiết
                return null;
            }
        }


		public async Task<Customers> GetCustomerByIdAsync(long? id)
        {
            try
            {
                return await _context.Customers.FirstOrDefaultAsync(c => c.ID == id);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và ghi nhật ký tại đây nếu cần thiết
                return null;
            }
        }
        public async Task<Customers> GetCustomerByOptions(string? name, string? email, string? phone, string? address)
        {
            try
            {
                return await _context.Customers.FirstOrDefaultAsync(c => c.Name == name && c.Email == email && c.Phone == phone && c.Address == address);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và ghi nhật ký tại đây nếu cần thiết
                return null;
            }
        }

        public async Task<bool> UpdateCustomerAsync(long id, Customers updatedCustomer)
        {
            try
            {
                if (id == updatedCustomer.ID)
                {
                    _context.Customers.Update(updatedCustomer);
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

        //    public async Task<bool> AddCustomerImagesAsync(ImageCar imageCar)
        //    {
        //        try
        //        {
        //            if (!(await IsExistImageCarsAsync(imageCar)))
        //            {
        //                _context.Add(imageCar);
        //                await _context.SaveChangesAsync();
        //            }
        //            return true;
        //        }
        //        catch (Exception ex)
        //        {
        //            // Xử lý ngoại lệ và ghi nhật ký tại đây nếu cần thiết
        //            return false;
        //        }
        //    }

        //    public async Task<bool> UpdateCarImagesAsync(long id, ImageCar updatedImageCar)
        //    {
        //        try
        //        {
        //            if (id == updatedImageCar.Car_Id)
        //            {
        //                _context.ImageCar.Update(updatedImageCar);
        //                await _context.SaveChangesAsync();
        //                return true;
        //            }
        //            return false;
        //        }
        //        catch (Exception ex)
        //        {
        //            // Xử lý ngoại lệ và ghi nhật ký tại đây nếu cần thiết
        //            return false;
        //        }
        //    }

        //    public async Task<bool> DeleteCarImagesAsync(long id)
        //    {
        //        try
        //        {
        //            var imgCar = await _context.ImageCar.FirstOrDefaultAsync(c => c.Id == id);
        //            if (imgCar != null)
        //            {
        //                _context.Remove(imgCar);
        //                await _context.SaveChangesAsync();
        //                return true;
        //            }
        //            return false;
        //        }
        //        catch (Exception ex)
        //        {
        //            // Xử lý ngoại lệ và ghi nhật ký tại đây nếu cần thiết
        //            return false;
        //        }
        //    }

        //    public async Task<bool> AddCarImagesAsync(long? Car_ID, string? img_path)
        //    {
        //        try
        //        {
        //            ImageCar imageCar = new ImageCar();
        //            imageCar.Car_Id = (int)Car_ID;
        //            imageCar.Image_URL = img_path;
        //            return await AddCarImagesAsync(imageCar);
        //        } catch (Exception ex)
        //        {
        //            return false;
        //        }

        //    }

        //    public async Task<bool> IsExistImageCarsAsync(ImageCar imageCar)
        //    {
        //        var res = await _context.ImageCar.AnyAsync(img => img.Car_Id == imageCar.Car_Id && img.Image_URL == imageCar.Image_URL);
        //        return res;
        //    }

        //    public async Task<List<Car>> GetCarsByOptionsAsync(string? condition, string? make, string? model, string? maxMileage, int? dateFrom, int? dateTo, string? priceFrom, string? priceTo, string? body)
        //    {
        //        try
        //        {
        //            var cars = await GetAllCarsAsync();
        //            var result = cars;
        //            if (condition != null)
        //            {
        //                result = cars.Where(c => c.Car_Condition.Equals(condition)).ToList();
        //            }
        //            if (make != null)
        //            {
        //                result = result.Where(c => c.Manufacturer.Equals(make)).ToList();
        //            }
        //            if (model != null)
        //            {
        //                result = result.Where(c => c.Model.Equals((string)model)).ToList();
        //            }
        //            if (dateFrom != null || dateTo != null)
        //            {
        //                if (dateFrom == null) dateFrom = DateTime.MinValue.Year;
        //                if (dateTo == null) dateTo = DateTime.MaxValue.Year;
        //                result = result.Where(c => (c.Year >= dateFrom && c.Year <= dateTo)).ToList();
        //            }
        //            if (priceFrom != null || priceTo != null)
        //            {
        //                result = result.Where(c => (c.Price >= decimal.Parse(priceFrom) && c.Price <= decimal.Parse(priceTo))).ToList();
        //            }
        //            if (body != null)
        //            {
        //                result = result.Where(c => c.Body.Equals(body)).ToList();
        //            }
        //            return result;
        //        }
        //        catch (Exception ex)
        //        {
        //            // Xử lý ngoại lệ và ghi nhật ký tại đây nếu cần thiết
        //            return null;
        //        }
        //    }
    }
}
