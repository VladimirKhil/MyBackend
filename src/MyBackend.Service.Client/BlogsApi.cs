using MyBackend.Service.Contract;
using MyBackend.Service.Contract.Models;
using System.Net.Http.Json;

namespace MyBackend.Service.Client;

public sealed class BlogsApi : IBlogsApi
{
    private readonly HttpClient _client;

    /// <summary>
    /// Initializes a new instance of <see cref="BlogsApi" /> class.
    /// </summary>
    /// <param name="client">HTTP client to use.</param>
    public BlogsApi(HttpClient client) => _client = client;

    public async Task<BlogEntriesPage> GetBlogEntriesAsync(string? tag = null, int from = 0, int count = 10, CancellationToken cancellationToken = default)
    {
        var tagParam = string.IsNullOrEmpty(tag) ? "" : $"tag={tag}&";
        return (await _client.GetFromJsonAsync<BlogEntriesPage>($"blogs?{tagParam}from={from}&count={count}", cancellationToken)) ?? new BlogEntriesPage();
    }
}
