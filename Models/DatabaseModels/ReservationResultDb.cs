namespace Car.Reservation.Api.Models.DatabaseModels
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class ReservationResultDb
    {
        [Column("reserved_car_id")]
        public int ReservedCarId { get; set; }
    }
}
