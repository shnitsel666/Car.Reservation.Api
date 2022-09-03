namespace Cars.Reservation.Api.Services.Domain.UserService
{
    using Cars.Reservation.Api.Models.Application.DataModels;

    public interface IUsersService
    {
        UserInfo GetUserInfo();
    }
}
