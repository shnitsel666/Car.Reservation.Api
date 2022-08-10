namespace Car.Reservation.Api.Services.ReservationService
{
    using Car.Reservation.Api.Models.ApiModels;
    using Car.Reservation.Api.Models.DatabaseModels;
    using Car.Reservation.Api.Models.Events;

    public interface IReservationService
    {
        ReservedCarInfoDb ReserveCar(ReserveCarRequest reserveCarRequest);

        string ValidateRequest(ReserveCarRequest reserveCarRequest);

        event EventHandler<ReservationEventArgs> OnCarReserved;
    }
}
