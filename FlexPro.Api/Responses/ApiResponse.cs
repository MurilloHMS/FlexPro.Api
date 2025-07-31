using System.Net;

namespace FlexPro.Api.Responses;

public class ApiResponse<T>
{
    public ApiResponse(HttpStatusCode StatusCode, string Message, T Data)
    {
        this.StatusCode = StatusCode;
        this.Message = Message;
        this.Data = Data;
    }

    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}