namespace Car.Reservation.Api.Models.CommonModels
{
    public class ApiResponseAsync<T>
    {
        /// <summary>
        /// Gets or sets operation statuses:
        /// Exception = -1
        /// Succes = 0
        /// Another error = N
        /// </summary>
        public int Code { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public static async Task<ApiResponseAsync<T>> DoMethodAsync(Func<ApiResponseAsync<T>, Task<ApiResponseAsync<T>>> action, Action<Exception>? errorHandler = default)
        {
            ApiResponseAsync<T> result = new();
            try
            {
                await action(result);
            }
            catch (Exception e)
            {
                errorHandler?.Invoke(e);
                result.Code = -1;
                result.Message = e.Message;
            }

            return result;
        }
    }
}
