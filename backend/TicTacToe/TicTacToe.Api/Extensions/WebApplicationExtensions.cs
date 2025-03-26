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
        services.AddAuth0ManagementApiClient(builder.Configuration);
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAngular",
                policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });
        services.AddAuthentication(builder.Configuration);
        services.AddAuthorization();
        services.ConfigureAuth0(builder.Configuration);
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

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors("AllowAngular");
        app.MapEndpoints();
    }

    private static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGameEndpoints();
        app.MapAuthEndpoints();
    }
}