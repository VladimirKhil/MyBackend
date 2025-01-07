using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBackend.Service.Contract;
using Polly;
using Polly.Extensions.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace MyBackend.Service.Client;

/// <summary>
/// Provides an extension method for adding <see cref="IMyBackendServiceClient" /> implementation to service collection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds <see cref="IMyBackendServiceClient" /> implementation to service collection.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="configuration">App configuration.</param>
    public static IServiceCollection AddMyBackendServiceClient(this IServiceCollection services, IConfiguration configuration)
    {
        var optionsSection = configuration.GetSection(MyBackendClientOptions.ConfigurationSectionName);
        services.Configure<MyBackendClientOptions>(optionsSection);

        var options = optionsSection.Get<MyBackendClientOptions>();

        services.AddHttpClient<IMyBackendServiceClient, MyBackendServiceClient>(
            client =>
            {
                if (options != null)
                {
                    var serviceUri = options.ServiceUri;
                    client.BaseAddress = serviceUri != null ? new Uri(serviceUri, "api/v1/") : null;
                    client.Timeout = options.Timeout;

                    if (options.Culture != null)
                    {
                        client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(options.Culture));
                    }

                    SetAuthSecret(options, client);
                }

                client.DefaultRequestVersion = HttpVersion.Version20;
            })
            .AddPolicyHandler(HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(
                    options?.RetryCount ?? MyBackendClientOptions.DefaultRetryCount,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(1.5, retryAttempt))));

        return services;
    }

    private static void SetAuthSecret(MyBackendClientOptions options, HttpClient client)
    {
        if (options.ClientSecret == null)
        {
            return;
        }

        var authHeader = Convert.ToBase64String(Encoding.ASCII.GetBytes($"admin:{options.ClientSecret}"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeader);
    }
}
