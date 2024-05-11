using LinqToDB;
using MyBackend.Database;
using MyBackend.Service.Contract.Models;
using MyBackend.Service.Contracts;

namespace MyBackend.Service.Services;

public sealed class NewsService(MyBackendConnection connection) : INewsService
{
    public Task AddNewsAsync(NewsItem newsItem, CancellationToken cancellationToken) =>
        connection.News.InsertWithInt32IdentityAsync(
            () => new Database.Models.NewsModel
            {
                DateTime = newsItem.DateTime,
                Text = newsItem.Text
            },
            cancellationToken);

    public async Task<NewsItem[]> GetNewsAsync(int year, CancellationToken cancellationToken)
    {
        var news = await connection.News.Where(n => n.DateTime.Year == year).OrderByDescending(n => n.DateTime).ToListAsync(cancellationToken);
        return news.Select(n => new NewsItem(n.DateTime, n.Text ?? "")).ToArray();
    }

    public Task<int[]> GetYearsAsync(CancellationToken cancellationToken) =>
        connection.News.Select(n => n.DateTime.Year).Distinct().OrderByDescending(y => y).ToArrayAsync(cancellationToken);
}
