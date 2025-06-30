using System.Text.Json;
using MyECommerceApi.Domain.Models;
using MyECommerceApi.Domain.Exceptions;
using MyECommerceApi.Domain.Constants.Common.Error;

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
            _logger.LogError(ex, Error.DefaultExceptionMessage);
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        int statusCode;
        string message;

        if (exception is NotFoundException)
            statusCode = StatusCodes.Status404NotFound;
        else if (exception is ValidationException)
            statusCode = StatusCodes.Status400BadRequest;
        else if (exception is UnauthorizedAccessException)
            statusCode = StatusCodes.Status401Unauthorized;
        else if (exception is DuplicateRecordException)
            statusCode = StatusCodes.Status409Conflict;
        else
            statusCode = StatusCodes.Status500InternalServerError;

        message = (statusCode == StatusCodes.Status500InternalServerError && !_env.IsDevelopment())
                ? Error.DefaultExceptionMessage 
                : exception.Message;

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