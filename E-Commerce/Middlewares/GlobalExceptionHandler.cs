using Domain.Exceptions.Product;
using Domain.Exceptions.Review;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken token)
    {
        _logger.LogError(exception, "Unhandled exception occurred.");

        var statusCode = exception switch
        {
            ReviewNotFoundException => StatusCodes.Status404NotFound,
            ProductNotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        var problem = new ProblemDetails
        {
            Title = "Something went wrong",
            Status = statusCode,
            Detail = exception.Message
        };

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(problem, token);
        return true;
    }
}
