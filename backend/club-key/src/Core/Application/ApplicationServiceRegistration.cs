namespace Application;

using System.Reflection;
using Abstractions.Behaviours;
using AutoMapper;
using FluentValidation;
using Mappings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Automapper
        var mapperConfig = new MapperConfiguration(mc => {
            mc.AddProfile(new MappingProfile());
        });

        var mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);
        
        // FluentValidation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // Pipelines
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        return services;
    }
}
