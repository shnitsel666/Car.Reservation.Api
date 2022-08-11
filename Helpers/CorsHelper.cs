namespace Car.Reservation.Api.Helpers
{
    public static class CorsHelper
    {
        public static string CorsPolicyName = "corsPolicy";

        public static string[] GetOrigins(string originsConfig = null)
        {
            string allowedHosts = originsConfig;
            if (string.IsNullOrEmpty(allowedHosts))
            {
                List<string> origins = new();
                origins.Add("http://localhost:3000");
                return origins.ToArray();
            }

            return allowedHosts.Replace(" ", string.Empty).Split(",");
        }
    }
}
