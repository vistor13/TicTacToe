using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicTacToe.Infrastructure.DataBase;

namespace TicTacToe.IntegrationTests;

public sealed class CustomWebFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        var host = base.CreateHost(builder);

        var scopedServices = host.Services.CreateScope().ServiceProvider;
        EnsureDbInitialized(scopedServices);

        return host;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor =
                services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            if (descriptor != null) services.Remove(descriptor);

            services.AddDbContext<ApplicationDbContext>((_, options) =>
            {
                var testDatabaseName = Guid.NewGuid().ToString();
                options.UseInMemoryDatabase(testDatabaseName);
            }, ServiceLifetime.Singleton, ServiceLifetime.Singleton);
        });
    }

    private static void EnsureDbInitialized(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.EnsureCreated();
    }
}