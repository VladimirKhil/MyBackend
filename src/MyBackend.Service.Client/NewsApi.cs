using MyBackend.Service.Contract;
using MyBackend.Service.Contract.Models;
using MyBackend.Service.Contract.Response;
using System.Net.Http.Json;

namespace MyBackend.Service.Client;

/// <ingeritdoc />
internal sealed class NewsApi : INewsApi
{
    private readonly HttpClient _client;

    /// <summary>
    /// Initializes a new instance of <see cref="NewsApi" /> class.
    /// </summary>
    /// <param name="client">HTTP client to use.</param>
    public NewsApi(HttpClient client) => _client = client;

    public async Task<NewsItem[]> GetNewsAsync(int year, CancellationToken cancellationToken = default) =>
        (await _client.GetFromJsonAsync<NewsResponse>($"news/years/{year}", cancellationToken))?.News ?? Array.Empty<NewsItem>();

    public async Task<int[]> GetYearsAsync(CancellationToken cancellationToken = default) =>
        (await _client.GetFromJsonAsync<YearsResponse>("news/years", cancellationToken))?.Years ?? Array.Empty<int>();
}
