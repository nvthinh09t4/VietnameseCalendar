using System.Net;

namespace webapi.Models
{
    public class BaseResponse<T> where T : class, new()
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public T Data { get; set; } = new T();
    }
}
