namespace Car.Reservation.Api.MapperProfiles
{
    using AutoMapper;
    using Car.Reservation.Api.Models.DatabaseModels;
    using Car.Reservation.Api.Models.Models;

    public class CarReservationApiProfile : Profile
    {
        public CarReservationApiProfile()
        {
            CreateMap<CarInfoDb, CarInfo>();
            CreateMap<ReservedCarInfoDb, ReservedCarInfo>();
            CreateMap<UserInfoDb, UserInfoDb>();
        }
    }
}
