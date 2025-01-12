using MyBackend.Service.Contract.Models;
using MyBackend.Service.Contract.Request;

namespace MyBackend.Service.Contract;

/// <summary>
/// Provides admin API.
/// </summary>
public interface IAdminApi
{
    /// <summary>
    /// Adds blog entry.
    /// </summary>
    /// <param name="blogEntryCreateRequest">Blog entry to add.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task<int> AddEntryAsync(BlogEntryCreateRequest blogEntryCreateRequest, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds news item.
    /// </summary>
    /// <param name="newsItem">News item to add.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task AddNewsAsync(NewsItem newsItem, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds tag.
    /// </summary>
    /// <param name="tagValue">Tag value to add.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task<int> AddTagAsync(string tagValue, CancellationToken cancellationToken = default);
}
