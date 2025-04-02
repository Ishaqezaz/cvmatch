using System;


// not a proper factory, but will do for now
namespace api.Common
{
    public class ServiceResponse<T>
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public ServiceErrorCode ErrorCode { get; set; }

        public ServiceResponse(string message, bool success, T? data, ServiceErrorCode? code)
        {
            Message = message;
            IsSuccess = success;
            Data = data;
            ErrorCode = code ?? ServiceErrorCode.None;
        }

        public static ServiceResponse<T> Success(string message, T? data)
            => new ServiceResponse<T>(message, true, data, default);
        
        public static ServiceResponse<T> Fail(string message, ServiceErrorCode code)
            => new ServiceResponse<T>(message, false, default, code);
    }
}