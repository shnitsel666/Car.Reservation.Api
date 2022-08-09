namespace Car.Reservation.Api.Extensions.ServiceCollection
{
    using Car.Reservation.Api.MapperProfiles;
    using Car.Reservation.Api.Repositories;
    using Car.Reservation.Api.Repositories.CarReservationApiRepository;
    using Car.Reservation.Api.Services.CarReservationApiService;
    using Car.Reservation.Api.Services.ReservationService;
    using Car.Reservation.Api.Services.UserService;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CarReservationApiProfile));
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IDBManager, DBManager>();
            services.AddSingleton<ICarReservationApiRepository, CarReservationApiRepository>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<DBManager, DBManager>();
            services.AddSingleton<ICarReservationApiService, CarReservationApiService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IReservationService, ReservationService>();
            return services;
        }
    }
}
