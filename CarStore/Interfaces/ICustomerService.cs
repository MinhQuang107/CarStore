using CarStore.Models;

namespace CarStore.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Customers>> GetAllCustomersAsync();
        Task<Customers> GetCustomerByIdAsync(long? id);
        Task<Customers> GetCustomerByOptions(string? name, string? email, string? phone, string? address);
        Task<bool> AddCustomerAsync(Customers customer);
        Task<bool> UpdateCustomerAsync(long id, Customers updatedCustomer);
        Task<bool> DeleteCustomerAsync(long id);
		//Task<List<Customer>> GetCustomersByOptionsAsync(string? condition, string? make, string? model, string? maxMileage, int? dateFrom, int? dateTo, string? priceFrom, string? priceTo, string? body);

		// Images Customer Services
		//Task<bool> AddCarImagesAsync(ImageCar imageCar);
  //      Task<bool> AddCarImagesAsync(long? Car_ID, string? img_path);
  //      Task<bool> UpdateCarImagesAsync(long id, ImageCar updatedImageCar);
  //      Task<bool> DeleteCarImagesAsync(long id);

    }
}
