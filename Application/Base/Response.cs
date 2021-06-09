using System.Collections.Generic;
using System.Net;

namespace Application.Base
{
    public abstract class BaseResponse
    {
        public string Message { get; set; }
        public HttpStatusCode Code { get; set; }
        public bool State { get; set; }
        public IDictionary<string, IEnumerable<string>> Errors { get; set; }
    }

    public class Response<T> : BaseResponse, IResponse<T>
    {
        public T Data { get; set; }

        public static Response<T> CreateResponseFailed(string message, HttpStatusCode code) 
        {
            return new Response<T>
            {
                Message = message,
                Code = code,
                State = false
            };
        }

        public static Response<T> CreateResponseSuccess(string message, HttpStatusCode code, T data)
        {
            return new Response<T>
            {
                Message = message,
                Code = code,
                Data = data,
                State = true
           
            };
        }

        public static Response<T> CreateResponseSuccess(string message, HttpStatusCode code)
        {
            return new Response<T>
            {
                Message = message,
                Code = code,
                State = true
            };
        }

    }
}