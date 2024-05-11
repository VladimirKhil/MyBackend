using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MyBackend.Database;
using MyBackend.Service.Configuration;
using MyBackend.Service.Contracts;
using MyBackend.Service.Metrics;
using MyBackend.Service.Services;

namespace MyBackend.ComponentTests;

internal abstract class TestsBase
{
    protected INewsService NewsService { get; }

    public TestsBase()
    {
        var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
        var configuration = builder.Build();

        var services = new ServiceCollection();

        services.Configure<MyBackendOptions>(configuration.GetSection(MyBackendOptions.ConfigurationSectionName));

        services.AddMyBackendDatabase(configuration);

        var meters = new OtelMetrics();
        services.AddSingleton(meters);

        services.AddSingleton<ILoggerFactory, NullLoggerFactory>();
        services.AddSingleton<ILogger<NewsService>, NullLogger<NewsService>>();
        services.AddTransient<INewsService, NewsService>();

        var serviceProvider = services.BuildServiceProvider();

        NewsService = serviceProvider.GetRequiredService<INewsService>();
    }
}