namespace Cars.Reservation.Api.Repositories
{
    using System.Data;
    using Npgsql;

    public class DBManager : IDBManager
    {
        private readonly ILogger<DBManager> _logger;
        private readonly IConfiguration _configuration;

        private List<IDbConnection> DefaultConnections { get; set; }

        private int DefaultConnectionsCount { get; set; } = 0;

        private readonly int DefaultConnectionsLimit = 20;

        public IDbConnection DefaultConnection
        {
            get
            {
                var connection = DefaultConnections[DefaultConnectionsCount++ % DefaultConnectionsLimit];
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Dispose();
                    DefaultConnections.Remove(connection);
                    IDbConnection newDB = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                    DefaultConnections.Add(newDB);
                    return newDB;
                }

                if (connection == null)
                {
                    IDbConnection newDB = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                    DefaultConnections.Add(newDB);
                    return newDB;
                }

                return DefaultConnections[DefaultConnectionsCount++ % DefaultConnectionsLimit];
            }

            private set
            {
            }
        }

        public DBManager(IConfiguration configuration, ILogger<DBManager> logger)
        {
            _logger = logger;
            _configuration = configuration;
            try
            {
                DefaultConnections = new List<IDbConnection>();
                for (int i = 0; i < DefaultConnectionsLimit; i++)
                {
                    IDbConnection db = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                    DefaultConnections.Add(db);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Program was stopped because of exception");
            }
        }
    }
}
