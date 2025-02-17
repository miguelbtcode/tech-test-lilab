namespace Presentation.Middlewares;

using Application.Exceptions;
using Errors;
using Newtonsoft.Json;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _environment;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";

            var statusCode = StatusCodes.Status500InternalServerError;
            var result = string.Empty;

            switch (ex)
            {
                case NotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    break;

                case ValidationException validationException:
                    statusCode = StatusCodes.Status400BadRequest;
                    var validationJson = JsonConvert.SerializeObject(validationException.Errors);
                    result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, ex.Message, validationJson));
                    break;

                case BadRequestException:
                    statusCode = StatusCodes.Status400BadRequest;
                    break;
            }

            if(string.IsNullOrEmpty(result))
                result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, ex.Message, ex.StackTrace));

            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(result);
        }
    }
}