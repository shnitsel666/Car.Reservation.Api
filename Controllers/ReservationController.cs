namespace Cars.Reservation.Api.Controllers
{
    using Cars.Reservation.Api.Models.Application.ApiModels;
    using Cars.Reservation.Api.Models.Application.DataModels;
    using Cars.Reservation.Api.Models.CommonModels;
    using Cars.Reservation.Api.Services.Application.CarsReservationApiService;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api")]
    public class ReservationController : ControllerBase
    {
        private readonly ILogger<ReservationController> _logger;
        private readonly ICarsReservationApiService _carsReservationApiService;

        #region .ctor
        public ReservationController(ILogger<ReservationController> logger, ICarsReservationApiService carsReservationApiService)
        {
            _logger = logger;
            _carsReservationApiService = carsReservationApiService;
        }
        #endregion

        #region GetFreeCars()
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(Response<List<CarInfo>>), StatusCodes.Status200OK)]
        public IActionResult GetFreeCars() =>
            Ok(_carsReservationApiService.GetFreeCars());
        #endregion

        #region GetUserReservedCars()
        [HttpGet("[action]/{userId}")]
        [ProducesResponseType(typeof(Response<List<ReservedCarInfo>>), StatusCodes.Status200OK)]
        public IActionResult GetUserReservedCars(int userId) =>
            Ok(_carsReservationApiService.GetUserReservedCars(userId));
        #endregion

        #region ReserveCar()
        [HttpPut("[action]")]
        [ProducesResponseType(typeof(Response<ReservedCarInfo>), StatusCodes.Status200OK)]
        public IActionResult ReserveCar(ReserveCarRequest reserveCarRequest) =>
            Ok(_carsReservationApiService.ReserveCar(reserveCarRequest));
        #endregion

        #region CancelCarReserve()
        [HttpPut("[action]/{reserveId}")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        public IActionResult CancelCarReserve(int reserveId) =>
            Ok(_carsReservationApiService.CancelCarReserve(reserveId));
        #endregion
    }
}