namespace Car.Reservation.Api.Models.ApiModels
{
    public class ReserveCarRequest
    {
        public int CarId { get; set; }

        public int UserId { get; set; }

        public DateTime ReservationDate { get; set; } = DateTime.Now;

        public int ReservationMinutes { get; set; }
    }
}
