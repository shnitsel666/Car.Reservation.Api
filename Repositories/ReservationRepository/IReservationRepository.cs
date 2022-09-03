namespace Cars.Reservation.Api.Repositories.CarsReservationApiRepository
{
    using Cars.Reservation.Api.Models.Application.ApiModels;
    using Cars.Reservation.Api.Models.Domain.DatabaseModels;

    public interface IReservationRepository
    {
        List<CarInfoDb> GetFreeCars();

        List<ReservedCarInfoDb> GetUserReservedCars(int userId);

        ReservedCarInfoDb CheckCarReservation(int carId);

        bool CancelCarReserve(int reserveId);

        public ReservedCarInfoDb ReserveCar(ReserveCarRequest reserveCarRequest);
    }
}
