namespace Cars.Reservation.Api.Models.Application.ApiModels
{
    public class ReserveCarRequest
    {
        public int CarId { get; set; }

        public int UserId { get; set; }

        public DateTime ReservationDateTime { get; set; } = DateTime.Now;

        public int ReservationMinutes { get; set; }
    }
}
