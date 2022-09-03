namespace Cars.Reservation.Api.Models.Application.DataModels
{
    public class ReservedCarInfo
    {
        public int ReservedCarId { get; set; }

        public int CarId { get; set; }

        public int UserId { get; set; }

        public DateTime ReservationDateTime { get; set; }

        public int ReservationMinutes { get; set; }

        public string SerialNumber { get; set; }

        public string Model { get; set; }

        public string UserName { get; set; }

        public string CarMakerName { get; set; }
    }
}
