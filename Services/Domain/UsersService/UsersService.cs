namespace Cars.Reservation.Api.Services.Domain.UserService
{
    using AutoMapper;
    using Cars.Reservation.Api.Models.Application.DataModels;
    using Cars.Reservation.Api.Models.Domain.DatabaseModels;

    public class UsersService : IUsersService
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        #region .ctor
        public UsersService(
            IConfiguration configuration,
            IMapper mapper,
            ILogger<UsersService> logger)
        {
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
        }
        #endregion

        #region GetUserInfo()
        public UserInfo GetUserInfo()
        {
            // Here should be jwt authorization functional and getting result from db
            var userDb = new UserInfoDb()
            {
                UserId = 1,
            };
            return _mapper.Map<UserInfo>(userDb);
        }
        #endregion
    }
}
