using CarStore.Models;

namespace CarStore.Interfaces
{
    public interface ICarService
    {
        Task<List<Car>> GetAllCarsAsync();
        Task<List<Car>> GetUsedCarsAsync();
        Task<List<Car>> GetNewCarsAsync();
        Task<Car> GetCarByIdAsync(long? id);
        Task<bool> AddCarAsync(Car car);
        Task<bool> UpdateCarAsync(long id, Car updatedCar);
        Task<bool> DeleteCarAsync(long id);
		Task<List<Car>> GetCarsByOptionsAsync(string? condition, string? make, string? model, string? maxMileage, int? dateFrom, int? dateTo, string? priceFrom, string? priceTo, string? body);

		// Images Car Services
		Task<bool> AddCarImagesAsync(ImageCar imageCar);
        Task<bool> AddCarImagesAsync(long? Car_ID, string? img_path);
        Task<bool> UpdateCarImagesAsync(long id, ImageCar updatedImageCar);
        Task<bool> DeleteCarImagesAsync(long id);

    }
}
