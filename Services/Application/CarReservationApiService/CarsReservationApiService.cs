namespace Cars.Reservation.Api.Services.Application.CarsReservationApiService
{
    using AutoMapper;
    using Cars.Reservation.Api.Helpers;
    using Cars.Reservation.Api.Models.Application.ApiModels;
    using Cars.Reservation.Api.Models.Application.DataModels;
    using Cars.Reservation.Api.Models.CommonModels;
    using Cars.Reservation.Api.Repositories.CarsReservationApiRepository;
    using Cars.Reservation.Api.Services.Domain.CarsService;
    using Cars.Reservation.Api.Services.Domain.ReservationService;
    using Cars.Reservation.Api.Services.Domain.UserService;

    public class CarsReservationApiService : ICarsReservationApiService
    {
        private readonly IMapper _mapper;
        private readonly IReservationRepository _carsReservationApiRepository;
        private readonly IReservationService _reservationService;
        private readonly ICarsService _carsService;
        private readonly IUsersService _userService;

        #region .ctor
        public CarsReservationApiService(
            IMapper mapper,
            IReservationRepository carsReservationApiRepository,
            IReservationService reservationService,
            ICarsService carsService,
            IUsersService userService)
        {
            _mapper = mapper;
            _carsReservationApiRepository = carsReservationApiRepository;
            _userService = userService;
            _carsService = carsService;
            _reservationService = reservationService;
        }
        #endregion

        #region AddCar()
        public Response<bool> AddCar(AddCarRequest addCarRequest) =>
            Response<bool>.DoMethod(
            resp =>
            {
                resp.Data = _carsService.AddCar(addCarRequest);
            },
            ErrorsHandlers.OnErrorPrintConsole);
        #endregion

        #region UpdateCar()
        public Response<bool> UpdateCar(UpdateCarRequest updateCarRequest) =>
            Response<bool>.DoMethod(
            resp =>
            {
                resp.Data = _carsService.UpdateCar(updateCarRequest);
            },
            ErrorsHandlers.OnErrorPrintConsole);
        #endregion

        #region RemoveCar()
        public Response<bool> RemoveCar(int carId) =>
            Response<bool>.DoMethod(
            resp =>
            {
                resp.Data = _carsService.RemoveCar(carId);
            },
            ErrorsHandlers.OnErrorPrintConsole);
        #endregion

        #region GetCar()
        public Response<CarInfo> GetCar(int carId) =>
            Response<CarInfo>.DoMethod(
            resp =>
            {
                var result = _carsService.GetCar(carId);
                var carInfo = _mapper.Map<CarInfo>(result);
                resp.Data = carInfo;
            },
            ErrorsHandlers.OnErrorPrintConsole);
        #endregion

        #region GetAllCars()
        public Response<List<CarInfo>> GetAllCars() =>
            Response<List<CarInfo>>.DoMethod(
            resp =>
            {
                var result = _carsService.GetAllCars();
                var cars = _mapper.Map<List<CarInfo>>(result);
                resp.Data = cars;
            },
            ErrorsHandlers.OnErrorPrintConsole);
        #endregion

        #region GetFreeCars()
        public Response<List<CarInfo>> GetFreeCars() =>
            Response<List<CarInfo>>.DoMethod(
            resp =>
            {
                var result = _reservationService.GetFreeCars();
                var cars = _mapper.Map<List<CarInfo>>(result);
                resp.Data = cars;
            },
            ErrorsHandlers.OnErrorPrintConsole);
        #endregion

        #region GetUserReservedCars()
        public Response<List<ReservedCarInfo>> GetUserReservedCars(int userId) =>
            Response<List<ReservedCarInfo>>.DoMethod(
            resp =>
            {
                var result = _carsReservationApiRepository.GetUserReservedCars(userId);
                var reservedCars = _mapper.Map<List<ReservedCarInfo>>(result);
                resp.Data = reservedCars;
            },
            ErrorsHandlers.OnErrorPrintConsole);
        #endregion

        #region ReserveCar()
        public Response<ReservedCarInfo> ReserveCar(ReserveCarRequest reserveCarRequest) =>
            Response<ReservedCarInfo>.DoMethod(
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

        #region CancelCarReserve()
        public Response<bool> CancelCarReserve(int reserveId) =>
            Response<bool>.DoMethod(
            resp =>
            {
                resp.Data = _reservationService.CancelCarReserve(reserveId);
            },
            ErrorsHandlers.OnErrorPrintConsole);
        #endregion
    }
}
