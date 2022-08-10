namespace Car.Reservation.Api.Services.UserService
{
    using AutoMapper;
    using Car.Reservation.Api.Models.DatabaseModels;
    using Car.Reservation.Api.Models.Models;

    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        #region .ctor
        public UserService(
            IConfiguration configuration,
            IMapper mapper,
            ILogger<UserService> logger)
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
