using System;

// not a proper factory, but will do for now
namespace api.Common
{
    public class ServiceResponse<T>
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }

        public ServiceResponse(string message, bool success, T? data)
        {
            Message = message;
            IsSuccess = success;
            Data = data;
        }

        public static ServiceResponse<T> Success(string message, T data)
            => new ServiceResponse<T>(message, true, data);
        
        public static ServiceResponse<T> Fail(string message)
            => new ServiceResponse<T>(message, false, default);
    }
}