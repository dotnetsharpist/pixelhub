using PixelHub.Service.Exceptions;
using PIxelHub.Service.Exceptions;

namespace PixelHub.WebApi.Middlewares;

public class ExceptionHandlerMiddleware
{
    public readonly RequestDelegate request;
    public readonly ILogger<ExceptionHandlerMiddleware> logger;
    public ExceptionHandlerMiddleware(RequestDelegate request, ILogger<ExceptionHandlerMiddleware> logger)
    {
        this.request = request;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await this.request(context);
        }
        catch (NotFoundException ex)
        {
            context.Response.StatusCode = ex.StatusCode;
        }
        catch (AlreadyExistException ex)
        {
            context.Response.StatusCode = ex.StatusCode;
        }
        catch (CustomException ex)
        {
            context.Response.StatusCode = ex.StatusCode;
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            this.logger.LogError(ex.ToString());
            
        }
    }
}
