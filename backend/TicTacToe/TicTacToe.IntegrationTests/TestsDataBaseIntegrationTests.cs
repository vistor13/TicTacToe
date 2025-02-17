using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using TicTacToe.Api;
using TicTacToe.Infrastructure.DataBase;

namespace TicTacToe.IntegrationTests;

public abstract class TestsDataBaseIntegrationTests
    : IClassFixture<CustomWebFactory<Program>>
{
    protected readonly HttpClient Client;

    private protected TestsDataBaseIntegrationTests(CustomWebFactory<Program> factory)
    {
        Client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

        ResetInMemoryDb(factory);
    }

    private static void ResetInMemoryDb(CustomWebFactory<Program> factory)
    {
        var scopedServices = factory.Services.CreateScope().ServiceProvider;
        var dbContext = scopedServices.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }
}