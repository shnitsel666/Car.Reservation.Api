namespace Car.Reservation.Api.Controllers
{
    using Car.Reservation.Api.Models.ApiModels;
    using Car.Reservation.Api.Models.CommonModels;
    using Car.Reservation.Api.Models.Models;
    using Car.Reservation.Api.Services.CarReservationApiService;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api")]
    public class CarReservationApiController : ControllerBase
    {
        private readonly ILogger<CarReservationApiController> _logger;
        private readonly ICarReservationApiService _carReservationApiService;

        #region .ctor
        public CarReservationApiController(ILogger<CarReservationApiController> logger, ICarReservationApiService carReservationApiService)
        {
            _logger = logger;
            _carReservationApiService = carReservationApiService;
        }
        #endregion

        #region AddCar()
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public IActionResult AddCar(AddCarRequest addCarRequest) =>
            Ok(_carReservationApiService.AddCar(addCarRequest));
        #endregion

        #region UpdateCar()
        [HttpPost("[action]/{carId}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public IActionResult UpdateCar(UpdateCarRequest updateCarRequest) =>
            Ok(_carReservationApiService.UpdateCar(updateCarRequest));
        #endregion

        #region RemoveCar()
        [HttpDelete("[action]/{carId}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public IActionResult RemoveCar(int carId) =>
            Ok(_carReservationApiService.RemoveCar(carId));
        #endregion

        #region GetCar()
        [HttpGet("[action]/{carId}")]
        [ProducesResponseType(typeof(ApiResponse<CarInfo>), StatusCodes.Status200OK)]
        public IActionResult GetCar(int carId) =>
            Ok(_carReservationApiService.GetCar(carId));
        #endregion

        #region GetAllCars()
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(List<CarInfo>), StatusCodes.Status200OK)]
        public IActionResult GetAllCars() =>
            Ok(_carReservationApiService.GetAllCars());
        #endregion

        #region GetUserReservedCars()
        [HttpGet("[action]/{userId}")]
        [ProducesResponseType(typeof(ApiResponse<List<ReservedCarInfo>>), StatusCodes.Status200OK)]
        public IActionResult GetUserReservedCars(int userId) =>
            Ok(_carReservationApiService.GetUserReservedCars(userId));
        #endregion

        #region ReserveCar()
        [HttpPut("[action]")]
        [ProducesResponseType(typeof(ApiResponse<ReservedCarInfo>), StatusCodes.Status200OK)]
        public IActionResult ReserveCar(ReserveCarRequest reserveCarRequest) =>
            Ok(_carReservationApiService.ReserveCar(reserveCarRequest));
        #endregion
    }
}