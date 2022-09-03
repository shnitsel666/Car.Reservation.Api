namespace Cars.Reservation.Api.Services.Domain.ReservationService
{
    using Cars.Reservation.Api.Models.Application.ApiModels;
    using Cars.Reservation.Api.Models.Application.DataModels;
    using Cars.Reservation.Api.Models.Application.Events;
    using Cars.Reservation.Api.Models.Domain.DatabaseModels;
    using Cars.Reservation.Api.Repositories.CarsReservationApiRepository;

    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public event EventHandler<ReservationEventArgs> OnCarReserved;

        #region .ctor
        public ReservationService(
            IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }
        #endregion

        #region ValidateRequest
        public string ValidateRequest(ReserveCarRequest reserveCarRequest)
        {
            var message = string.Empty;
            if (reserveCarRequest.ReservationMinutes > 120)
            {
                message = "Car can be reserved for maximum 120 minutes";
            }

            if (reserveCarRequest.ReservationDateTime > DateTime.Now.AddHours(24))
            {
                message = "Car can be reserved maximum in 24 hours";
            }

            if (reserveCarRequest.ReservationDateTime > DateTime.Now.AddHours(24))
            {
                message = "Car can be reserved maximum in 24 hours";
            }

            if (string.IsNullOrEmpty(message))
            {
                var reservationInfo = _reservationRepository.CheckCarReservation(reserveCarRequest.CarId);
                if (reservationInfo != null)
                {
                    message = "This car is under the reservation";
                }
            }

            return message;
        }
        #endregion

        #region GetFreeCars()
        public List<CarInfoDb> GetFreeCars()
        {
            return _reservationRepository.GetFreeCars();
        }
        #endregion

        #region ReserveCar
        public ReservedCarInfoDb ReserveCar(ReserveCarRequest reserveCarRequest)
        {
            var reservedCarInfo = _reservationRepository.ReserveCar(reserveCarRequest);
            CarInfo carInfo = new()
            {
                CarId = reservedCarInfo.CarId,
                SerialNumber = reservedCarInfo.SerialNumber,
                Model = reservedCarInfo.Model,
                CarMakerName = reservedCarInfo.CarMakerName,
            };

            // OnReservedCarNotification(carInfo);
            return reservedCarInfo;
        }
        #endregion

        #region CanselCarReserve
        public bool CancelCarReserve(int reserveId)
        {
            return _reservationRepository.CancelCarReserve(reserveId);
        }
        #endregion

        #region OnReservedCarNotification
        protected virtual void OnReservedCarNotification(CarInfo carInfo)
        {
            if (carInfo != null)
            {
                OnCarReserved(this, new ReservationEventArgs() { CarInfo = carInfo });
            }
        }
        #endregion
    }
}
