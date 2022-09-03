namespace Cars.Reservation.Api.Extensions.ServiceCollection
{
    using Cars.Reservation.Api.MapperProfiles;
    using Cars.Reservation.Api.Repositories;
    using Cars.Reservation.Api.Repositories.CarsRepository;
    using Cars.Reservation.Api.Repositories.CarsReservationApiRepository;
    using Cars.Reservation.Api.Services.Application.CarsReservationApiService;
    using Cars.Reservation.Api.Services.Domain.CarsService;
    using Cars.Reservation.Api.Services.Domain.ReservationService;
    using Cars.Reservation.Api.Services.Domain.UserService;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Diagnostics.HealthChecks;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CarsReservationApiProfile));
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IDBManager, DBManager>();
            services.AddSingleton<IReservationRepository, ReservationRepository>();
            services.AddSingleton<ICarsRepository, CarsRepository>();
            return services;
        }

        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddSingleton<DBManager, DBManager>();
            services.AddSingleton<IUsersService, UsersService>();
            services.AddSingleton<ICarsService, CarsService>();
            services.AddSingleton<IReservationService, ReservationService>();
            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<ICarsReservationApiService, CarsReservationApiService>();
            return services;
        }

        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services)
        {
            var hcBuilder = services.AddHealthChecks();
            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());
            return services;
        }
    }
}
