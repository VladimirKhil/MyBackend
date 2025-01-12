using MyBackend.Service.Contract;
using MyBackend.Service.Contract.Models;
using MyBackend.Service.Contract.Response;
using System.Net.Http.Json;

namespace MyBackend.Service.Client;

internal sealed class TagsApi : ITagsApi
{
    private readonly HttpClient _client;

    /// <summary>
    /// Initializes a new instance of <see cref="TagsApi" /> class.
    /// </summary>
    /// <param name="client">HTTP client to use.</param>
    public TagsApi(HttpClient client) => _client = client;

    public async Task<Tag[]> GetTagsAsync(CancellationToken cancellationToken = default) =>
        (await _client.GetFromJsonAsync<TagsResponse>("tags", cancellationToken))?.Tags ?? Array.Empty<Tag>();
}
