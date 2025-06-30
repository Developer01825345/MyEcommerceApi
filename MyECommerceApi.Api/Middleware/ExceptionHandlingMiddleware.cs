using System.Text.Json;
using MyECommerceApi.Api.Models;
using MyECommerceApi.Domain.Exceptions;

namespace MyECommerceApi.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        int statusCode;
        string message;

        if (exception is NotFoundException)
        {
            statusCode = StatusCodes.Status404NotFound;
            message = exception.Message;
        }
        else if (exception is ValidationException)
        {
            statusCode = StatusCodes.Status400BadRequest;
            message = exception.Message;
        }
        else if (exception is UnauthorizedAccessException)
        {
            statusCode = StatusCodes.Status401Unauthorized;
            message = exception.Message;
        }
        else if (exception is DuplicateRecordException)
        {
            statusCode = StatusCodes.Status409Conflict;
            message = exception.Message;
        }
        else
        {
            statusCode = StatusCodes.Status500InternalServerError;
            message = _env.IsDevelopment() ? exception.Message : "An unexpected error occurred.";
        }

        context.Response.StatusCode = statusCode;

        var response = new ApiErrorResponse
        {
            StatusCode = statusCode,
            Message = message
        };

        var result = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(result);
    }
}