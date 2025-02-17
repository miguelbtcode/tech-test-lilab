namespace Application.Abstractions.Behaviours;

using MediatR;
using Messaging;
using Microsoft.Extensions.Logging;
using Domain.Abstractions;

public class LoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseCommand
{
    private readonly ILogger<TRequest> _logger;

    public LoggingBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
        )
    {
        var name = request.GetType().Name;
        _logger.LogInformation("Ejecutando el comando {CommandName}", name);

        TResponse result;
        try
        {
            result = await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "El comando {CommandName} tuvo un error inesperado", name);
            throw;
        }

        // Check if the result is failure
        if (result is Result { IsFailure: true } res)
        {
            _logger.LogWarning(
                "El comando {CommandName} falló con error: {Error}",
                name,
                res.Error.Name
            );
        }

        _logger.LogInformation("El comando {CommandName} se ejecutó exitosamente", name);
        return result;
    }
}