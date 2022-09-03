namespace Cars.Reservation.Api.MapperProfiles
{
    using AutoMapper;
    using Cars.Reservation.Api.Models.Application.DataModels;
    using Cars.Reservation.Api.Models.Domain.DatabaseModels;

    public class CarsReservationApiProfile : Profile
    {
        public CarsReservationApiProfile()
        {
            CreateMap<CarInfoDb, CarInfo>();
            CreateMap<ReservedCarInfoDb, ReservedCarInfo>();
            CreateMap<UserInfoDb, UserInfoDb>();
        }
    }
}
