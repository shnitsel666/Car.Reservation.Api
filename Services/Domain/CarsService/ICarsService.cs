namespace Cars.Reservation.Api.Services.Domain.CarsService
{
    using Cars.Reservation.Api.Models.Application.ApiModels;
    using Cars.Reservation.Api.Models.Application.DataModels;
    using Cars.Reservation.Api.Models.CommonModels;

    public interface ICarsService
    {
        bool AddCar(AddCarRequest addCarRequest);

        bool UpdateCar(UpdateCarRequest updateCarRequest);

        bool RemoveCar(int carId);

        CarInfo GetCar(int carId);

        List<CarInfo> GetAllCars();
    }
}
