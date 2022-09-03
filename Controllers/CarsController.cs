namespace Cars.Reservation.Api.Controllers
{
    using Cars.Reservation.Api.Models.Application.ApiModels;
    using Cars.Reservation.Api.Models.Application.DataModels;
    using Cars.Reservation.Api.Models.CommonModels;
    using Cars.Reservation.Api.Services.Application.CarsReservationApiService;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api")]
    public class CarsController : ControllerBase
    {
        private readonly ILogger<CarsController> _logger;
        private readonly ICarsReservationApiService _carsReservationApiService;

        #region .ctor
        public CarsController(ILogger<CarsController> logger, ICarsReservationApiService carsReservationApiService)
        {
            _logger = logger;
            _carsReservationApiService = carsReservationApiService;
        }
        #endregion

        #region AddCar()
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        public IActionResult AddCar(AddCarRequest addCarRequest) =>
            Ok(_carsReservationApiService.AddCar(addCarRequest));
        #endregion

        #region UpdateCar()
        [HttpPost("[action]/{carId}")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        public IActionResult UpdateCar(UpdateCarRequest updateCarRequest) =>
            Ok(_carsReservationApiService.UpdateCar(updateCarRequest));
        #endregion

        #region RemoveCar()
        [HttpDelete("[action]/{carId}")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        public IActionResult RemoveCar(int carId) =>
            Ok(_carsReservationApiService.RemoveCar(carId));
        #endregion

        #region GetCar()
        [HttpGet("[action]/{carId}")]
        [ProducesResponseType(typeof(Response<CarInfo>), StatusCodes.Status200OK)]
        public IActionResult GetCar(int carId) =>
            Ok(_carsReservationApiService.GetCar(carId));
        #endregion

        #region GetAllCars()
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(List<CarInfo>), StatusCodes.Status200OK)]
        public IActionResult GetAllCars() =>
            Ok(_carsReservationApiService.GetAllCars());
        #endregion
    }
}