using System.Net;

namespace PixelHub.Service.Exceptions;

public class TooManyRequestException : ClientExeption
{
    public override HttpStatusCode StatusCode { get; } = HttpStatusCode.TooManyRequests;

    public override string TitleMessage { get; protected set; } = String.Empty;
}
