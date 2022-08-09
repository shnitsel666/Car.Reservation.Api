namespace Car.Reservation.Api.Services.CarReservationApiService
{
    using Car.Reservation.Api.Models.ApiModels;
    using Car.Reservation.Api.Models.CommonModels;
    using Car.Reservation.Api.Models.Models;

    public interface ICarReservationApiService
    {
        ApiResponse<bool> AddCar(AddCarRequest addCarRequest);

        ApiResponse<bool> UpdateCar(UpdateCarRequest updateCarRequest);

        ApiResponse<bool> RemoveCar(int carId);

        ApiResponse<CarInfo> GetCar(int carId);

        ApiResponse<List<CarInfo>> GetAllCars();

        ApiResponse<List<ReservedCarInfo>> GetUserReservedCars(int userId);

        ApiResponse<ReservedCarInfo> ReserveCar(ReserveCarRequest reserveCarRequest);
    }
}
