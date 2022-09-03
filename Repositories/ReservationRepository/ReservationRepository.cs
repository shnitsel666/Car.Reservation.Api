namespace Cars.Reservation.Api.Repositories.CarsReservationApiRepository
{
    using Cars.Reservation.Api.Models.Application.ApiModels;
    using Cars.Reservation.Api.Models.Domain.DatabaseModels;
    using Dapper;

    public class ReservationRepository : IReservationRepository
    {
        private readonly DBManager _DBManager;

        #region .ctor
        public ReservationRepository(DBManager DBManager)
        {
            _DBManager = DBManager;
        }
        #endregion

        #region GetFreeCars()
        public List<CarInfoDb> GetFreeCars()
        {
            var sql = @"SELECT car_id AS CarId, serial_number AS SerialNumber, model, status, insert_date_time AS InsertDateTime, maker_id AS MakerId, car_maker_name AS CarMakerName
                FROM public.cars
                INNER JOIN cars_makers
                ON cars_makers.car_maker_id = cars.maker_id
                WHERE status = true AND car_id NOT IN 
                (SELECT car_id
                FROM public.reserved_cars WHERE is_reserved = true)
                ORDER BY car_id DESC";
            var result = _DBManager.DefaultConnection.Query<CarInfoDb>(sql);
            return result.ToList();
        }
        #endregion

        #region GetUserReservedCars()
        public List<ReservedCarInfoDb> GetUserReservedCars(int userId)
        {
            var sql = @"SELECT reserved_car_id AS ReservedCarId, reserved_cars.car_id AS CarId, reserved_cars.user_id AS UserId, 
                reservation_date_time AS ReservationDateTime, reservation_minutes AS ReservationMinutes, serial_number AS SerialNumber, model, 
                user_name AS UserName, car_maker_name AS CarMakerName
                FROM public.reserved_cars
                INNER JOIN cars
                ON cars.car_id = reserved_cars.car_id AND status = true
                INNER JOIN cars_makers
                ON cars_makers.car_maker_id = cars.maker_id
                INNER JOIN users
                ON users.user_id = reserved_cars.user_id
                WHERE reserved_cars.user_id = @UserId AND is_reserved = true
                ORDER BY reserved_cars.car_id DESC";
            var result = _DBManager.DefaultConnection.Query<ReservedCarInfoDb>(sql, new { UserId = userId });
            return result.ToList();
        }
        #endregion

        #region CheckCarReservation()
        public ReservedCarInfoDb CheckCarReservation(int carId)
        {
            var sql = @"SELECT reserved_car_id, car_id, user_id, reservation_date_time, reservation_minutes
                FROM public.reserved_cars
                WHERE car_id = @CarId AND reservation_date_time >= now() - INTERVAL '1 day' AND reservation_date_time < now() + INTERVAL '1 day'  AND is_reserved = true
                limit 1";
            var result = _DBManager.DefaultConnection.QueryFirstOrDefault<ReservedCarInfoDb>(sql, new { CarId = carId });
            return result;
        }
        #endregion

        #region ReserveCar()
        public ReservedCarInfoDb ReserveCar(ReserveCarRequest reserveCarRequest)
        {
            var reserveCarSql = @"INSERT INTO public.reserved_cars(
	            car_id, user_id, reservation_date_time, reservation_minutes)
	            VALUES (@CarId, @UserId, @ReservationDateTime, @ReservationMinutes)
                RETURNING reserved_car_id AS ReservedCarId";
            var reserveCarResult = _DBManager.DefaultConnection.QuerySingle<ReservationResultDb>(reserveCarSql, new
            {
                reserveCarRequest.CarId,
                reserveCarRequest.UserId,
                reserveCarRequest.ReservationDateTime,
                reserveCarRequest.ReservationMinutes,
            });
            var reservedCarSql = @"SELECT reserved_car_id AS ReservedCarId, reserved_cars.car_id AS CarId, reserved_cars.user_id AS UserId, 
                reservation_date_time AS ReservationDateTime, reservation_minutes AS ReservationMinutes, serial_number AS SerialNumber, model, 
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

        #region CanselCarReserve()
        public bool CancelCarReserve(int reserveId)
        {
            var cancelCarReserveSql = @"UPDATE public.reserved_cars
	           SET is_reserved=false
               WHERE reserved_car_id = @ReservedCarId";
            var cancelCarReserveResult = _DBManager.DefaultConnection.Execute(cancelCarReserveSql, new
            {
                ReservedCarId = reserveId,
            });
            return cancelCarReserveResult == 1;
        }
        #endregion
    }
}
