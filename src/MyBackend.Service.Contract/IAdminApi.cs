using MyBackend.Service.Contract.Models;

namespace MyBackend.Service.Contract;

/// <summary>
/// Provides admin API.
/// </summary>
public interface IAdminApi
{
    /// <summary>
    /// Adds news item.
    /// </summary>
    /// <param name="newsItem">News item to add.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task AddNewsAsync(NewsItem newsItem, CancellationToken cancellationToken = default);
}
