namespace Cars.Reservation.Api.Repositories.CarsRepository
{
    using Cars.Reservation.Api.Models.Application.ApiModels;
    using Cars.Reservation.Api.Models.Domain.DatabaseModels;
    using Dapper;

    public class CarsRepository : ICarsRepository
    {
        private readonly DBManager _DBManager;

        #region .ctor
        public CarsRepository(DBManager DBManager)
        {
            _DBManager = DBManager;
        }
        #endregion

        #region AddCar()
        public bool AddCar(AddCarRequest addCarRequest)
        {
            var sql = @"INSERT INTO public.cars(
	            serial_number, model, status, insert_date_time, maker_id)
	            VALUES (@SerialNumber, @Model, true, @InsertDateTime, @MakerId)";
            var result = _DBManager.DefaultConnection.Execute(sql, new
            {
                addCarRequest.SerialNumber,
                addCarRequest.Model,
                addCarRequest.Status,
                InsertDateTime = DateTime.Now,
                addCarRequest.MakerId,
            });
            return result == 1;
        }
        #endregion

        #region UpdateCar()
        public bool UpdateCar(UpdateCarRequest updateCarRequest)
        {
            var sql = @"UPDATE public.cars
	            SET serial_number = @SerialNumber, model = @Model, status = @Status, insert_date_time = @InsertDateTime, maker_id = @MakerId
	            WHERE car_id = @CarId AND status = true";
            var result = _DBManager.DefaultConnection.Execute(sql, new
            {
                updateCarRequest.CarId,
                updateCarRequest.SerialNumber,
                updateCarRequest.Model,
                updateCarRequest.Status,
                InsertDateTime = DateTime.Now,
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
            var sql = @"SELECT car_id AS CarId, serial_number AS SerialNumber, model, status, insert_date_time AS InsertDateTime, maker_id AS MakerId, car_maker_name AS CarMakerName
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
            var sql = @"SELECT car_id AS CarId, serial_number AS SerialNumber, model, status, insert_date_time AS InsertDateTime, maker_id AS MakerId, car_maker_name AS CarMakerName
	            FROM public.cars
                INNER JOIN cars_makers
                ON cars_makers.car_maker_id = cars.maker_id
				WHERE status = true
                ORDER BY car_id DESC";
            var result = _DBManager.DefaultConnection.Query<CarInfoDb>(sql);
            return result.ToList();
        }
        #endregion
    }
}
