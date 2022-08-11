namespace Car.Reservation.Api.Services.CarReservationApiService
{
    using AutoMapper;
    using Car.Reservation.Api.Helpers;
    using Car.Reservation.Api.Models.ApiModels;
    using Car.Reservation.Api.Models.CommonModels;
    using Car.Reservation.Api.Models.Models;
    using Car.Reservation.Api.Repositories.CarReservationApiRepository;
    using Car.Reservation.Api.Services.ReservationService;
    using Car.Reservation.Api.Services.UserService;

    public class CarReservationApiService : ICarReservationApiService
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly ICarReservationApiRepository _carReservationApiRepository;
        private readonly IReservationService _reservationService;
        private readonly IUserService _userService;

        #region .ctor
        public CarReservationApiService(
            IConfiguration configuration,
            IMapper mapper,
            ILogger<ICarReservationApiRepository> logger,
            ICarReservationApiRepository carReservationApiRepository,
            IReservationService reservationService,
            IUserService userService)
        {
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
            _carReservationApiRepository = carReservationApiRepository;
            _userService = userService;
            _reservationService = reservationService;
        }
        #endregion

        #region AddCar()
        public ApiResponse<bool> AddCar(AddCarRequest addCarRequest) =>
            ApiResponse<bool>.DoMethod(
            resp =>
            {
                resp.Data = _carReservationApiRepository.AddCar(addCarRequest);
            },
            ErrorsHandlers.OnErrorPrintConsole);
        #endregion

        #region UpdateCar()
        public ApiResponse<bool> UpdateCar(UpdateCarRequest updateCarRequest) =>
            ApiResponse<bool>.DoMethod(
            resp =>
            {
                resp.Data = _carReservationApiRepository.UpdateCar(updateCarRequest);
            },
            ErrorsHandlers.OnErrorPrintConsole);
        #endregion

        #region RemoveCar()
        public ApiResponse<bool> RemoveCar(int carId) =>
            ApiResponse<bool>.DoMethod(
            resp =>
            {
                resp.Data = _carReservationApiRepository.RemoveCar(carId);
            },
            ErrorsHandlers.OnErrorPrintConsole);
        #endregion

        #region GetCar()
        public ApiResponse<CarInfo> GetCar(int carId) =>
            ApiResponse<CarInfo>.DoMethod(
            resp =>
            {
                var result = _carReservationApiRepository.GetCar(carId);
                var carInfo = _mapper.Map<CarInfo>(result);
                resp.Data = carInfo;
            },
            ErrorsHandlers.OnErrorPrintConsole);
        #endregion

        #region GetAllCars()
        public ApiResponse<List<CarInfo>> GetAllCars() =>
            ApiResponse<List<CarInfo>>.DoMethod(
            resp =>
            {
                var result = _carReservationApiRepository.GetAllCars();
                var cars = _mapper.Map<List<CarInfo>>(result);
                resp.Data = cars;
            },
            ErrorsHandlers.OnErrorPrintConsole);
        #endregion

        #region GetUserReservedCars()
        public ApiResponse<List<ReservedCarInfo>> GetUserReservedCars(int userId) =>
            ApiResponse<List<ReservedCarInfo>>.DoMethod(
            resp =>
            {
                var result = _carReservationApiRepository.GetUserReservedCars(userId);
                var reservedCars = _mapper.Map<List<ReservedCarInfo>>(result);
                resp.Data = reservedCars;
            },
            ErrorsHandlers.OnErrorPrintConsole);
        #endregion

        #region ReserveCar()
        public ApiResponse<ReservedCarInfo> ReserveCar(ReserveCarRequest reserveCarRequest) =>
            ApiResponse<ReservedCarInfo>.DoMethod(
            resp =>
            {
                var message = _reservationService.ValidateRequest(reserveCarRequest);
                if (string.IsNullOrEmpty(message))
                {
                    var reservationResult = _reservationService.ReserveCar(reserveCarRequest);
                    var reservedCarInfo = _mapper.Map<ReservedCarInfo>(reservationResult);

                    // _reservationService.OnCarReserved();
                    resp.Data = reservedCarInfo;
                }
                else
                {
                    resp.Code = 1;
                    resp.Message = message;
                }
            },
            ErrorsHandlers.OnErrorPrintConsole);
        #endregion
    }
}
