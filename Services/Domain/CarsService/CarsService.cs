namespace Cars.Reservation.Api.Services.Domain.CarsService
{
    using AutoMapper;
    using Cars.Reservation.Api.Models.Application.ApiModels;
    using Cars.Reservation.Api.Models.Application.DataModels;
    using Cars.Reservation.Api.Repositories.CarsRepository;

    public class CarsService : ICarsService
    {
        private readonly IMapper _mapper;
        private readonly ICarsRepository _carsRepository;

        #region .ctor
        public CarsService(
            IMapper mapper,
            ICarsRepository carsRepository)
        {
            _mapper = mapper;
            _carsRepository = carsRepository;
        }
        #endregion

        #region AddCar()
        public bool AddCar(AddCarRequest addCarRequest)
        {
            return _carsRepository.AddCar(addCarRequest);
        }
        #endregion

        #region UpdateCar()
        public bool UpdateCar(UpdateCarRequest updateCarRequest)
        {
            return _carsRepository.UpdateCar(updateCarRequest);
        }
        #endregion

        #region RemoveCar()
        public bool RemoveCar(int carId)
        {
            return _carsRepository.RemoveCar(carId);
        }
        #endregion

        #region GetCar()
        public CarInfo GetCar(int carId)
        {
            var result = _carsRepository.GetCar(carId);
            return _mapper.Map<CarInfo>(result);
        }
        #endregion

        #region GetAllCars()
        public List<CarInfo> GetAllCars()
        {
            var result = _carsRepository.GetAllCars();
            return _mapper.Map<List<CarInfo>>(result);
        }
        #endregion
    }
}
