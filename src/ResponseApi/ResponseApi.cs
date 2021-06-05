using System;
using System.Collections.Generic;

namespace JuniorPorfirio.ResponseApi
{
    /// <summary>
    /// Base Class to manager ResponseApi
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseApi<T> : IResponseApi
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">Generic value</param>
        public ResponseApi( T value)
        {
            Value = value;
        }
        public ResponseApi()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status">StatusCode to return</param>
        public ResponseApi(ResponseApiStatusCode status) => Status = status;
        public T Value { get; }
        public string Message { get; private set; }

        public IDictionary<string, string> Errors { get; private set; } = new Dictionary<string,string>();

        public ResponseApiStatusCode Status { get; private set; } = ResponseApiStatusCode.Success;
        
        /// <summary>
        /// Custom validations
        /// </summary>
        /// <param name="value">Value to validate</param>
        /// <returns></returns>
        public static ResponseApi<T> Against(T value) => new ResponseApi<T>(value);

        /// <summary>
        /// Validate Error=500 or Success=200
        /// </summary>
        /// <param name="func">Function to validade</param>
        /// <returns></returns>
        public static IResponseApi IsError(Func<T> func)
        {
            try
            {
                var data = func();
                return Success(data);
            }
            catch (System.Exception ex)
            {

                return Error(ex.Message);
            }
        }

        /// <summary>
        /// Value 
        /// </summary>
        /// <returns></returns>
        public object GetValue() => Value;

        /// <summary>
        /// StatusCode Success = 200
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ResponseApi<T> Success(T value) => new ResponseApi<T>(value) { Status = ResponseApiStatusCode.Success };

        /// <summary>
        /// StatusCode Created = 201
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ResponseApi<T> Created(T value) => new ResponseApi<T>(value) { Status = ResponseApiStatusCode.Created };

        /// <summary>
        /// StatusCode ServerError = 500 with message
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns></returns>
        public static ResponseApi<T> Error(string message = "") => new ResponseApi<T>(ResponseApiStatusCode.Error) { Message = message };

        /// <summary>
        /// StatusCode Invalid = 400 with messages
        /// </summary>
        /// <param name="errors">Messages</param>
        /// <returns></returns>
        public static ResponseApi<T> Invalid(IDictionary<string,string> errors) => new ResponseApi<T>(ResponseApiStatusCode.Invalid) { Errors = errors };
        
        /// <summary>
        /// Unauthorized = 400 
        /// </summary>
        /// <returns></returns>
        public static ResponseApi<T> Unauthorized(string message="") => new ResponseApi<T>(ResponseApiStatusCode.Unauthorized) { Message = message};

        public static ResponseApi<T> NotFound() => new ResponseApi<T>(ResponseApiStatusCode.NotFound);
    }
}
