using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBackend.Service.Client;
using MyBackend.Service.Contract;

namespace MyBackend.IntegrationTests;

public abstract class TestsBase
{
    protected IMyBackendServiceClient MyBackendClient { get; private set; }

    public TestsBase()
    {
        var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
        var configuration = builder.Build();

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddMyBackendServiceClient(configuration);

        var serviceProvider = serviceCollection.BuildServiceProvider();
        MyBackendClient = serviceProvider.GetRequiredService<IMyBackendServiceClient>();
    }
}