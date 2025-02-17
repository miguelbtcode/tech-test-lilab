namespace Presentation.Extensions;

using System.Reflection;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;

public static class SwaggerExtensions
{
    public static void AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Club Key .NET 9.0 🚀",
                Description = "🚀 API collection for Club Key application 🏛️. This project provides a set of APIs to manage a club, allowing administrators to handle members, visitors, and access control efficiently. Features include user registration, authentication, entrance and exit tracking, and role-based permissions 🔐.\n\n" +
                              "🔹 **License:** MIT License 📜\n\n" +
                              "🔹 **GitHub Repository:** [Club Key API](https://github.com/miguelbtcode/tech-test-lilab) 💻",
                Version = "v1",
                Contact = new OpenApiContact
                {
                    Name = "miguelbtcode",
                    Url = new Uri("https://github.com/miguelbtcode"),
                    Email = "mabt2206@gmail.com"
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                },
                TermsOfService = new Uri("https://opensource.org/licenses/MIT")
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "\ud83d\udd11 Ingrese el token en este formato️: **{token}**",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        services.AddOpenApi();
    }

    public static void UseSwaggerUIConfig(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseReDoc(option =>
        {
            option.SpecUrl("/openapi/v1.json");
        });
        app.MapScalarApiReference();
    }
}