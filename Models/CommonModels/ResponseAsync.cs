namespace Cars.Reservation.Api.Models.CommonModels
{
    public class ResponseAsync<T>
    {
        #region Props

        /// <summary>
        /// Gets or sets operation statuses:
        /// Exception = -1
        /// Succes = 0
        /// Another error = N.
        /// </summary>
        public int Code { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
        #endregion

        #region DoMethodAsync(action)

        /// <summary>
        /// Wrapper delegate methods for cautching exceptions and custom handling for errors.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="action">Wrapper delegate method.</param>
        /// <returns>Returns result or error.</returns>
        public static async Task<ResponseAsync<T>> DoMethodAsync(Func<ResponseAsync<T>, Task<ResponseAsync<T>>> action)
        {
            ResponseAsync<T> result = new();
            try
            {
                await action(result);
            }
            catch (ResponseAsyncException e)
            {
                result.Code = -1;
                result.Message = e.Message;
            }
            catch (Exception e)
            {
                result.Code = -1;
                result.Message = e.Message;
            }

            return result;
        }
        #endregion

        #region DoMethodAsync(action, errorHandler)

        /// <summary>
        /// Wrapper delegate methods for cautching exceptions and custom handling for errors.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="action">Wrapper delegate method.</param>
        /// <param name="errorHandler">Wrapper delegate handler for errors.</param>
        /// <returns>Returns result or error.</returns>
        public static async Task<ResponseAsync<T>> DoMethodAsync(Func<ResponseAsync<T>, Task<ResponseAsync<T>>> action, Action<Exception>? errorHandler = default)
        {
            ResponseAsync<T> result = new();
            try
            {
                await action(result);
            }
            catch (ResponseAsyncException e)
            {
                errorHandler?.Invoke(e);
                result.Code = -1;
                result.Message = e.Message;
            }
            catch (Exception e)
            {
                errorHandler?.Invoke(e);
                result.Code = -1;
                result.Message = e.Message;
            }

            return result;
        }
        #endregion

        #region Throw(code, message)

        /// <summary>
        /// If error with specified code and message has to be thrown.
        /// </summary>
        /// <param name="code">Error code.</param>
        /// <param name="message">Error message.</param>
        /// <exceprion cref="ResponseAsyncException">Special exception</exception>
        public void Throw(int code, string message) =>
            throw new ResponseAsyncException(code, message);
        #endregion

        #region Throw(message)

        /// <summary>
        /// If error with specified message has to be thrown (Code = -1).
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <exceprion cref="ResponseAsyncException">Special exception.</exception>
        public void Throw(string message) =>
            throw new ResponseAsyncException(-1, message);
        #endregion

        #region GetResultIfNotError()

        /// <summary>
        /// Returns result if Code == 0.
        /// </summary>
        /// <returns>Data.</returns>
        /// <exceprion cref="ResponseAsyncException">Original error.</exception>
        public T GetResultIfNotError() =>
            GetResultIfNotError(string.Empty);
        #endregion

        #region GetResultIfNotError(errorMessage)

        /// <summary>
        /// Returns result if Code == 0.
        /// </summary>
        /// <param name="errorMessage">Error text will be added to start of message.</param>
        /// <returns>Data.</returns>
        /// <exceprion cref="ResponseAsyncException">Original error.</exception>
        public T GetResultIfNotError(string errorMessage)
        {
            if (Code != 0)
            {
                if (string.IsNullOrEmpty(errorMessage))
                {
                    Throw(Code, Message);
                }

                Throw(Code, $"{errorMessage.Trim()} {Message}");
            }

            return Data;
        }
        #endregion

        #region ResponseAsyncException
        public class ResponseAsyncException : Exception
        {
            public int Code { get; set; } = -1;

            public ResponseAsyncException(string message)
                : base(message)
            {
            }

            public ResponseAsyncException(int code, string message)
                : base(message)
            {
                this.Code = code;
            }
        }
        #endregion
    }
}
