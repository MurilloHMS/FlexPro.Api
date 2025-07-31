using System.Net;

namespace FlexPro.Api.Responses;

public class ApiResponse<T>
{
    public ApiResponse(HttpStatusCode statusCode, string message, T data)
    {
        StatusCode = statusCode;
        Message = message;
        Data = data;
    }

    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}