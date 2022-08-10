namespace Car.Reservation.Api.Models.ApiModels
{
    public class UpdateCarRequest
    {
        public int CarId { get; set; }

        public string SerialNumber { get; set; }

        public string Model { get; set; }

        public bool Status { get; set; } = true;

        public DateTime InsertDate { get; set; } = DateTime.Now;

        public int MakerId { get; set; }
    }
}
