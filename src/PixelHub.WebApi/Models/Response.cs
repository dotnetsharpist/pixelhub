namespace PixelHub.WebApi.Models;

public class Response
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public object Data { get; set; } = string.Empty;
}
