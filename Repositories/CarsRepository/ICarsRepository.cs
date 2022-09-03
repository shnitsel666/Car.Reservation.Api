namespace Cars.Reservation.Api.Repositories.CarsRepository
{
    using Cars.Reservation.Api.Models.Application.ApiModels;
    using Cars.Reservation.Api.Models.Domain.DatabaseModels;

    public interface ICarsRepository
    {
        bool AddCar(AddCarRequest addCarRequest);

        bool UpdateCar(UpdateCarRequest updateCarRequest);

        bool RemoveCar(int carId);

        CarInfoDb GetCar(int carId);

        List<CarInfoDb> GetAllCars();
    }
}
