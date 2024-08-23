using CarStore.Models;

namespace CarStore.Interfaces
{
    public interface IAppointmentService
    {
        Task<List<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment> GetAppointmentByIdAsync(long? id);
        Task<bool> AddAppointmentAsync(Appointment appointment);
        Task<bool> UpdateAppointmentAsync(long id, Appointment updatedAppointment);
        Task<bool> DeleteAppointmentAsync(long id);
		//Task<List<Appointment>> GetAppointmentsByOptionsAsync(string? condition, string? make, string? model, string? maxMileage, int? dateFrom, int? dateTo, string? priceFrom, string? priceTo, string? body);

		// Images Appointment Services
		//Task<bool> AddCarImagesAsync(ImageCar imageCar);
  //      Task<bool> AddCarImagesAsync(long? Car_ID, string? img_path);
  //      Task<bool> UpdateCarImagesAsync(long id, ImageCar updatedImageCar);
  //      Task<bool> DeleteCarImagesAsync(long id);

    }
}
