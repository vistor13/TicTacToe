using TicTacToe.Api.Endpoints;
using TicTacToe.Application;
using TicTacToe.Infrastructure;

namespace TicTacToe.Api.Extensions;

/// <summary>
///     Set of web application methods for configuring services and middleware.
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    ///     Adds application-specific services to the dependency injection container.
    ///     This includes API explorer, core services, and Swagger documentation setup.
    /// </summary>
    /// <param name="builder">The <see cref="WebApplicationBuilder" /> used to configure the application.</param>
    public static void AddApplicationServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddEndpointsApiExplorer();
        services.AddInfrastructure();
        services.AddApplicationLayer();
        services.AddSwaggerGenTicTacToe();
        services.AddDatabase(builder.Configuration);
    }

    /// <summary>
    ///     Configures the middleware pipeline for the web application.
    ///     Includes setup for developer tools, Swagger UI, and application endpoints.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication" /> used to build the application pipeline.</param>
    public static void UseApplicationMiddlewares(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });
        }

        app.MapEndpoints();
    }

    private static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGameEndpoints();
    }
}