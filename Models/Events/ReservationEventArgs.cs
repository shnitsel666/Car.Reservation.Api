namespace Car.Reservation.Api.Models.Events
{
    using Car.Reservation.Api.Models.Models;

    public class ReservationEventArgs : EventArgs
    {
        public CarInfo CarInfo { get; set; }
    }
}
