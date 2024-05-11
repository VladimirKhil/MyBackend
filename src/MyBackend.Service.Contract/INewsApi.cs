using MyBackend.Service.Contract.Models;

namespace MyBackend.Service.Contract;

/// <summary>
/// Provides API for working with news.
/// </summary>
public interface INewsApi
{
    /// <summary>
    /// Gets years having any news.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task<int[]> GetYearsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets news by year.
    /// </summary>
    /// <param name="year">News year.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task<NewsItem[]> GetNewsAsync(int year, CancellationToken cancellationToken = default);
}
