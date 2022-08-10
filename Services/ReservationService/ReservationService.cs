namespace Car.Reservation.Api.Services.ReservationService
{
    using AutoMapper;
    using Car.Reservation.Api.Models.ApiModels;
    using Car.Reservation.Api.Models.DatabaseModels;
    using Car.Reservation.Api.Models.Events;
    using Car.Reservation.Api.Models.Models;
    using Car.Reservation.Api.Repositories.CarReservationApiRepository;

    public class ReservationService : IReservationService
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly ICarReservationApiRepository _carReservationApiRepository;

        public event EventHandler<ReservationEventArgs> OnCarReserved;

        #region .ctor
        public ReservationService(
            IConfiguration configuration,
            IMapper mapper,
            ILogger<ICarReservationApiRepository> logger,
            ICarReservationApiRepository carReservationApiRepository)
        {
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
            _carReservationApiRepository = carReservationApiRepository;
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

            if (reserveCarRequest.ReservationDate > DateTime.Now.AddHours(24))
            {
                message = "Car can be reserved maximum in 24 hours";
            }

            if (reserveCarRequest.ReservationDate > DateTime.Now.AddHours(24))
            {
                message = "Car can be reserved maximum in 24 hours";
            }

            if (string.IsNullOrEmpty(message))
            {
                var reservationInfo = _carReservationApiRepository.CheckCarReservation(reserveCarRequest.CarId);
                if (reservationInfo != null)
                {
                    message = "This car is under the reservation";
                }
            }

            return message;
        }
        #endregion

        #region ReserveCar
        public ReservedCarInfoDb ReserveCar(ReserveCarRequest reserveCarRequest)
        {
            var reservedCarInfo = _carReservationApiRepository.ReserveCar(reserveCarRequest);
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
