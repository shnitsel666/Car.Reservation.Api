namespace Cars.Reservation.Api.Models.Domain.DatabaseModels
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class ReservationResultDb
    {
        [Column("reserved_car_id")]
        public int ReservedCarId { get; set; }
    }
}
