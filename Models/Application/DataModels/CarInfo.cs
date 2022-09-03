namespace Cars.Reservation.Api.Models.Application.DataModels
{
    public class CarInfo
    {
        public int CarId { get; set; }

        public string SerialNumber { get; set; }

        public string Model { get; set; }

        public bool Status { get; set; }

        public DateTime InsertDateTime { get; set; }

        public int MakerId { get; set; }

        public string CarMakerName { get; set; }
    }
}
