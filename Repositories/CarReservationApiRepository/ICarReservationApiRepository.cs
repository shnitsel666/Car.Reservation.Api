namespace Car.Reservation.Api.Repositories.CarReservationApiRepository
{
    using Car.Reservation.Api.Models.ApiModels;
    using Car.Reservation.Api.Models.DatabaseModels;

    public interface ICarReservationApiRepository
    {
        bool AddCar(AddCarRequest addCarRequest);

        bool UpdateCar(UpdateCarRequest updateCarRequest);

        bool RemoveCar(int carId);

        CarInfoDb GetCar(int carId);

        List<CarInfoDb> GetAllCars();

        List<CarInfoDb> GetFreeCars();

        List<ReservedCarInfoDb> GetUserReservedCars(int userId);

        ReservedCarInfoDb CheckCarReservation(int carId);

        public ReservedCarInfoDb ReserveCar(ReserveCarRequest reserveCarRequest);
    }
}
