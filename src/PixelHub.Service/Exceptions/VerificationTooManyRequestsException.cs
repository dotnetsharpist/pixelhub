namespace PixelHub.Service.Exceptions;

public class VerificationTooManyRequestsException : TooManyRequestException
{
    public VerificationTooManyRequestsException()
    {
        this.TitleMessage = "Verification too many requests!";
    }
}
