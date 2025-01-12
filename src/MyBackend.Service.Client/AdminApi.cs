using MyBackend.Service.Contract;
using MyBackend.Service.Contract.Models;
using MyBackend.Service.Contract.Request;
using System.Net;
using System.Net.Http.Json;

namespace MyBackend.Service.Client;

/// <ingeritdoc />
internal sealed class AdminApi : IAdminApi
{
    private readonly HttpClient _client;

    /// <summary>
    /// Initializes a new instance of <see cref="AdminApi" /> class.
    /// </summary>
    /// <param name="client">HTTP client to use.</param>
    public AdminApi(HttpClient client) => _client = client;

    public async Task AddNewsAsync(NewsItem newsItem, CancellationToken cancellationToken = default)
    {
        using var response = await _client.PostAsJsonAsync("admin/news", newsItem, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await GetErrorMessageAsync(response, cancellationToken);
            throw new Exception(errorMessage);
        }
    }

    public async Task<int> AddTagAsync(string tagValue, CancellationToken cancellationToken = default)
    {
        using var response = await _client.PostAsJsonAsync("admin/tags", new TagCreateRequest(tagValue), cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await GetErrorMessageAsync(response, cancellationToken);
            throw new Exception(errorMessage);
        }

        return await response.Content.ReadFromJsonAsync<int>(cancellationToken: cancellationToken);
    }

    public async Task<int> AddEntryAsync(BlogEntryCreateRequest blogEntryCreateRequest, CancellationToken cancellationToken = default)
    {
        using var response = await _client.PostAsJsonAsync("admin/blogs", blogEntryCreateRequest, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await GetErrorMessageAsync(response, cancellationToken);
            throw new Exception(errorMessage);
        }

        return await response.Content.ReadFromJsonAsync<int>(cancellationToken: cancellationToken);
    }

    private static async Task<string> GetErrorMessageAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        var serverError = await response.Content.ReadAsStringAsync(cancellationToken);

        if (response.StatusCode == HttpStatusCode.BadGateway)
        {
            return $"{response.StatusCode}: Bad Gateway";
        }

        if (response.StatusCode == HttpStatusCode.TooManyRequests)
        {
            return $"{response.StatusCode}: Too many requests. Try again later";
        }

        return $"{response.StatusCode}: {serverError}";
    }
}
