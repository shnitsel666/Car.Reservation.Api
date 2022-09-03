namespace Cars.Reservation.Api.Models.Application.Events
{
    using Cars.Reservation.Api.Models.Application.DataModels;

    public class ReservationEventArgs : EventArgs
    {
        public CarInfo CarInfo { get; set; }
    }
}
