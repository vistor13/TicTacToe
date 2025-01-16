using TicTacToe.Api.Game;

namespace TicTacToe.Api.Extensions;

public static class WebApplicationExtensions
{
    public static void AddApplicationServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddEndpointsApiExplorer();
        services.AddCore();
        services.AddSwaggerGen();
    }

    public static void UseApplicationMiddlewares(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseGameEndpoints();
    }

    private static void UseGameEndpoints(this IEndpointRouteBuilder app)
    {
        app.AddGameEndpoints();
    }
}