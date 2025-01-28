using Microsoft.OpenApi.Models;

namespace TicTacToe.Api.Extensions;

/// <summary>
///     Set of Swagger methods for configuring Swagger documentation.
/// </summary>
public static class SwaggerExtensions
{
    /// <summary>
    ///     Configures Swagger generation for e API, including XML comments and metadata.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> used to register application services.</param>
    public static void AddSwaggerGenTicTacToe(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            var fileName = typeof(Program).Assembly.GetName().Name + ".xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, fileName);
            c.IncludeXmlComments(xmlPath);

            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "TicTacToe Web Api",
                Version = "v1",
                Contact = new OpenApiContact
                {
                    Name = "Viktor Polishchuk",
                    Email = "polishchuk.viktor13@gmail.com",
                    Url = new Uri("https://www.linkedin.com/in/viktor-polishchuk/")
                }
            });
        });
    }
}