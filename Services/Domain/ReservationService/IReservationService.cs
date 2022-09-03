namespace Cars.Reservation.Api.Services.Domain.ReservationService
{
    using Cars.Reservation.Api.Models.Application.ApiModels;
    using Cars.Reservation.Api.Models.Application.Events;
    using Cars.Reservation.Api.Models.Domain.DatabaseModels;

    public interface IReservationService
    {
        List<CarInfoDb> GetFreeCars();

        ReservedCarInfoDb ReserveCar(ReserveCarRequest reserveCarRequest);

        bool CancelCarReserve(int reserveId);

        string ValidateRequest(ReserveCarRequest reserveCarRequest);

        event EventHandler<ReservationEventArgs> OnCarReserved;
    }
}
