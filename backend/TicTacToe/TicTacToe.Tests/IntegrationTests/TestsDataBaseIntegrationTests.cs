using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicTacToe.Api;
using TicTacToe.Infrastructure.DataBase;

namespace TicTacToe.Tests.IntegrationTests;

public abstract class TestsDataBaseIntegrationTests : IClassFixture<CustomWebFactory<Program>>, IDisposable
{
    private readonly IServiceScope _scope;
    protected readonly HttpClient Client;
    protected readonly IConfiguration Configuration;
    protected readonly ApplicationDbContext Context;
    protected readonly CustomWebFactory<Program> Factory;

    protected TestsDataBaseIntegrationTests(CustomWebFactory<Program> factory)
    {
        Factory = factory;
        _scope = Factory.Services.CreateScope();
        var serviceProvider = _scope.ServiceProvider;

        Context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        Configuration = serviceProvider.GetRequiredService<IConfiguration>();
        Client = Factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });

        Context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        _scope.Dispose();
    }
}