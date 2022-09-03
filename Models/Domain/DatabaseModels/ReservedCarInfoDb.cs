namespace Cars.Reservation.Api.Models.Domain.DatabaseModels
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class ReservedCarInfoDb
    {
        [Column("reserved_car_id")]
        public int ReservedCarId { get; set; }

        [Column("car_id")]
        public int CarId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("reservation_date_time")]
        public DateTime ReservationDateTime { get; set; }

        [Column("reservation_minutes")]
        public int ReservationMinutes { get; set; }

        [Column("serial_number")]
        public string SerialNumber { get; set; }

        [Column("model")]
        public string Model { get; set; }

        [Column("user_name")]
        public string UserName { get; set; }

        [Column("car_maker_name")]
        public string CarMakerName { get; set; }
    }
}
