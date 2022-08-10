namespace Car.Reservation.Api.Models.DatabaseModels
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class CarInfoDb
    {
        [Column("car_id")]
        public int CarId { get; set; }

        [Column("serial_number")]
        public string SerialNumber { get; set; }

        [Column("model")]
        public string Model { get; set; }

        [Column("status")]
        public bool Status { get; set; }

        [Column("insert_date")]
        public DateTime InsertDate { get; set; }

        [Column("maker_id")]
        public int MakerId { get; set; }

        [Column("car_maker_name")]
        public string CarMakerName { get; set; }
    }
}
