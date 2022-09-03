namespace Cars.Reservation.Api.Helpers
{
    public static class CorsHelper
    {
        public static readonly string CorsPolicyName = "corsPolicy";
        public static readonly string[] OriginsConfigDefault = { "http://localhost:3000" };

        public static string[] GetOrigins(string originsConfig)
        {
            string allowedHosts = originsConfig;
            if (string.IsNullOrEmpty(allowedHosts))
            {
                return OriginsConfigDefault;
            }

            return allowedHosts.Replace(" ", string.Empty).Split(",");
        }
    }
}
