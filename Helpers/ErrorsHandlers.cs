namespace Car.Reservation.Api.Helpers
{
    public static class ErrorsHandlers
    {
        public static void OnErrorPrintConsole(Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
    }
}
