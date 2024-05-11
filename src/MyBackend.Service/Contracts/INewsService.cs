using MyBackend.Service.Contract.Models;

namespace MyBackend.Service.Contracts;

public interface INewsService
{
    Task AddNewsAsync(NewsItem newsItem, CancellationToken cancellationToken = default);

    Task<int[]> GetYearsAsync(CancellationToken cancellationToken = default);

    Task<NewsItem[]> GetNewsAsync(int year, CancellationToken cancellationToken = default);
}
