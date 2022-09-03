namespace Cars.Reservation.Api.Services.Application.CarsReservationApiService
{
    using Cars.Reservation.Api.Models.Application.ApiModels;
    using Cars.Reservation.Api.Models.Application.DataModels;
    using Cars.Reservation.Api.Models.CommonModels;

    public interface ICarsReservationApiService
    {
        Response<bool> AddCar(AddCarRequest addCarRequest);

        Response<bool> UpdateCar(UpdateCarRequest updateCarRequest);

        Response<bool> RemoveCar(int carId);

        Response<CarInfo> GetCar(int carId);

        Response<List<CarInfo>> GetAllCars();

        Response<List<CarInfo>> GetFreeCars();

        Response<List<ReservedCarInfo>> GetUserReservedCars(int userId);

        Response<ReservedCarInfo> ReserveCar(ReserveCarRequest reserveCarRequest);

        Response<bool> CancelCarReserve(int reserveId);
    }
}
