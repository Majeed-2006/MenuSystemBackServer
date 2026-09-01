using App.API.GlobalExceptionHandler.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is BusinessException)
        {
            httpContext.Response.StatusCode = 400;

            await httpContext.Response.WriteAsJsonAsync(new
            {
                error = exception.Message
            }, cancellationToken);

            return true;
        }

        // Unknown/unexpected error
        httpContext.Response.StatusCode = 500;

        await httpContext.Response.WriteAsJsonAsync(new
        {
            error = "An unexpected error occurred."
        }, cancellationToken);

        return true;
    }
}