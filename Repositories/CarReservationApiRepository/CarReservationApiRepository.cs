namespace Car.Reservation.Api.Repositories.CarReservationApiRepository
{
    using AutoMapper;
    using Car.Reservation.Api.Models.ApiModels;
    using Car.Reservation.Api.Models.DatabaseModels;
    using Dapper;

    public class CarReservationApiRepository : ICarReservationApiRepository
    {
        private readonly IMapper _mapper;
        private readonly DBManager _DBManager;
        private readonly IConfiguration _configuration;

        #region .ctor
        public CarReservationApiRepository(IConfiguration configuration, IMapper mapper, DBManager DBManager)
        {
            _configuration = configuration;
            _mapper = mapper;
            _DBManager = DBManager;
        }
        #endregion

        #region AddCar()
        public bool AddCar(AddCarRequest addCarRequest)
        {
            var sql = @"INSERT INTO public.cars(
	                    serial_number, model, status, insert_date, maker_id)
	                    VALUES (@SerialNumber, @Model, true, @InsertDate, @MakerId)";
            var result = _DBManager.DefaultConnection.Execute(sql, new
            {
                addCarRequest.SerialNumber,
                addCarRequest.Model,
                addCarRequest.Status,
                InsertDate = DateTime.Now,
                addCarRequest.MakerId,
            });
            return result == 1;
        }
        #endregion

        #region UpdateCar()
        public bool UpdateCar(UpdateCarRequest updateCarRequest)
        {
            var sql = @"UPDATE public.cars
	                SET serial_number = @SerialNumber, model = @Model, status = @Status, insert_date = @InsertDate, maker_id = @MakerId
	                WHERE car_id = @CarId AND status = true";
            var result = _DBManager.DefaultConnection.Execute(sql, new
            {
                updateCarRequest.CarId,
                updateCarRequest.SerialNumber,
                updateCarRequest.Model,
                updateCarRequest.Status,
                InsertDate = DateTime.Now,
                updateCarRequest.MakerId,
            });
            return result == 1;
        }
        #endregion

        #region RemoveCar()
        public bool RemoveCar(int carId)
        {
            var sql = @"UPDATE public.cars
	                SET status = false
	                WHERE car_id = @CarId";
            var result = _DBManager.DefaultConnection.Execute(sql, new { CarId = carId });
            return result == 1;
        }
        #endregion

        #region GetCar()
        public CarInfoDb GetCar(int carId)
        {
            var sql = @"SELECT car_id AS CarId, serial_number AS SerialNumber, model, status, insert_date AS InsertDate, maker_id AS MakerId, car_maker_name AS CarMakerName
	                FROM public.cars
                    INNER JOIN cars_makers
                    ON cars_makers.car_maker_id = cars.maker_id
                    WHERE car_id = @CarId AND status = true
                    ORDER BY car_id DESC";
            var result = _DBManager.DefaultConnection.QuerySingle<CarInfoDb>(sql, new { CarId = carId });
            return result;
        }
        #endregion

        #region GetAllCars()
        public List<CarInfoDb> GetAllCars()
        {
            var sql = @"SELECT car_id AS CarId, serial_number AS SerialNumber, model, status, insert_date AS InsertDate, maker_id AS MakerId, car_maker_name AS CarMakerName
	                FROM public.cars
                    INNER JOIN cars_makers
                    ON cars_makers.car_maker_id = cars.maker_id
					WHERE status = true
                    ORDER BY car_id DESC";
            var result = _DBManager.DefaultConnection.Query<CarInfoDb>(sql);
            return result.ToList();
        }
        #endregion

        #region GetFreeCars()
        public List<CarInfoDb> GetFreeCars()
        {
            var sql = @"SELECT car_id AS CarId, serial_number AS SerialNumber, model, status, insert_date AS InsertDate, maker_id AS MakerId, car_maker_name AS CarMakerName
                    FROM public.cars
                    INNER JOIN cars_makers
                    ON cars_makers.car_maker_id = cars.maker_id
                    WHERE car_id NOT IN (SELECT car_id FROM reserved_cars) AND status = true
                    ORDER BY car_id DESC";
            var result = _DBManager.DefaultConnection.Query<CarInfoDb>(sql);
            return result.ToList();
        }
        #endregion

        #region GetUserReservedCars()
        public List<ReservedCarInfoDb> GetUserReservedCars(int userId)
        {
            var sql = @"SELECT reserved_car_id AS ReservedCarId, reserved_cars.car_id AS CarId, reserved_cars.user_id AS UserId, 
                    reservation_date AS ReservationDate, reservation_minutes AS ReservationMinutes, serial_number AS SerialNumber, model, 
                    user_name AS UserName, car_maker_name AS CarMakerName
                    FROM public.reserved_cars
                    INNER JOIN cars
                    ON cars.car_id = reserved_cars.car_id AND status = true
                    INNER JOIN cars_makers
                    ON cars_makers.car_maker_id = cars.maker_id
                    INNER JOIN users
                    ON users.user_id = reserved_cars.user_id
                    WHERE reserved_cars.user_id = @UserId AND reservation_date >= now()
                    ORDER BY reserved_cars.car_id DESC";
            var result = _DBManager.DefaultConnection.Query<ReservedCarInfoDb>(sql, new { UserId = userId });
            return result.ToList();
        }
        #endregion

        #region CheckCarReservation()
        public ReservedCarInfoDb CheckCarReservation(int carId)
        {
            var sql = @"SELECT reserved_car_id, car_id, user_id, reservation_date, reservation_minutes
                    FROM public.reserved_cars
                    WHERE car_id = @CarId AND reservation_date >= now() - INTERVAL '1 day' AND reservation_date < now() + INTERVAL '1 day'
                    limit 1";
            var result = _DBManager.DefaultConnection.QueryFirstOrDefault<ReservedCarInfoDb>(sql, new { CarId = carId });
            return result;
        }
        #endregion

        #region ReserveCar()
        public ReservedCarInfoDb ReserveCar(ReserveCarRequest reserveCarRequest)
        {
            var reserveCarSql = @"INSERT INTO public.reserved_cars(
	                car_id, user_id, reservation_date, reservation_minutes)
	                VALUES (@CarId, @UserId, @ReservationDate, @ReservationMinutes)
                    RETURNING reserved_car_id AS ReservedCarId";
            var reserveCarResult = _DBManager.DefaultConnection.QuerySingle<ReservationResultDb>(reserveCarSql, new
            {
                reserveCarRequest.CarId,
                reserveCarRequest.UserId,
                reserveCarRequest.ReservationDate,
                reserveCarRequest.ReservationMinutes,
            });
            var reservedCarSql = @"SELECT reserved_car_id AS ReservedCarId, reserved_cars.car_id AS CarId, reserved_cars.user_id AS UserId, 
                    reservation_date AS ReservationDate, reservation_minutes AS ReservationMinutes, serial_number AS SerialNumber, model, 
                    user_name AS UserName, car_maker_name AS CarMakerName
                    FROM public.reserved_cars
                    INNER JOIN cars
                    ON cars.car_id = reserved_cars.car_id AND status = true
                    INNER JOIN cars_makers
                    ON cars_makers.car_maker_id = cars.maker_id
                    INNER JOIN users
                    ON users.user_id = reserved_cars.user_id
                    WHERE reserved_car_id = @ReservedCarId
                    ORDER BY reserved_cars.car_id DESC";
            var reservedCarResult = _DBManager.DefaultConnection.QuerySingle<ReservedCarInfoDb>(reservedCarSql, new { reserveCarResult.ReservedCarId });
            return reservedCarResult;
        }
        #endregion
    }
}
