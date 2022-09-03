namespace Cars.Reservation.Api.Models.CommonModels
{
    public class Response<T>
    {
        #region Props

        /// <summary>
        /// Gets or sets operation statuses:
        /// Exception = -1
        /// Succes = 0
        /// Another error = N.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets message with warning or error.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets data with type T.
        /// </summary>
        public T Data { get; set; }
        #endregion

        #region DoMethod(action)

        /// <summary>
        /// Wrapper delegate methods for cautching exceptions and custom handling for errors.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="action">Wrapper delegate method.</param>
        /// <returns>Returns result or error.</returns>
        public static Response<T> DoMethod(Action<Response<T>> action)
        {
            Response<T> result = new();
            try
            {
                action(result);
            }
            catch (ResponseException e)
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

        #region DoMethod(action, errorHandler)

        /// <summary>
        /// Wrapper delegate methods for cautching exceptions and custom handling for errors.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="action">Wrapper delegate method.</param>
        /// <param name="errorHandler">Wrapper delegate handler for errors.</param>
        /// <returns>Returns result or error.</returns>
        public static Response<T> DoMethod(Action<Response<T>> action, Action<Exception>? errorHandler = default)
        {
            Response<T> result = new();
            try
            {
                action(result);
            }
            catch (ResponseException e)
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
        /// <exceprion cref="ResponseException">Special exception</exception>
        public void Throw(int code, string message) =>
            throw new ResponseException(code, message);
        #endregion

        #region Throw(message)

        /// <summary>
        /// If error with specified message has to be thrown (Code = -1).
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <exceprion cref="ResponseException">Special exception.</exception>
        public void Throw(string message) =>
            throw new ResponseException(-1, message);
        #endregion

        #region GetResultIfNotError()

        /// <summary>
        /// Returns result if Code == 0.
        /// </summary>
        /// <returns>Data.</returns>
        /// <exceprion cref="ResponseException">Original error.</exception>
        public T GetResultIfNotError() =>
            GetResultIfNotError(string.Empty);
        #endregion

        #region GetResultIfNotError(errorMessage)

        /// <summary>
        /// Returns result if Code == 0.
        /// </summary>
        /// <param name="errorMessage">Error text will be added to start of message.</param>
        /// <returns>Data.</returns>
        /// <exceprion cref="ResponseException">Original error.</exception>
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

        #region ResponseException
        public class ResponseException : Exception
        {
            public int Code { get; set; } = -1;

            public ResponseException(string message)
                : base(message)
            {
            }

            public ResponseException(int code, string message)
                : base(message)
            {
                this.Code = code;
            }
        }
        #endregion
    }
}
