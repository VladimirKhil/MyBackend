using MyBackend.Service.Contract.Models;

namespace MyBackend.IntegrationTests;

internal sealed class NewsApiTests : TestsBase
{
    internal static readonly int[] Years = [2023, 2024];

    [Test]
    public async Task AddNews_Ok()
    {
        var offset1 = new DateTimeOffset(2023, 04, 20, 12, 30, 0, TimeSpan.Zero);

        await MyBackendClient.Admin.AddNewsAsync(new NewsItem(offset1, "Test news"));
        await MyBackendClient.Admin.AddNewsAsync(new NewsItem(new DateTimeOffset(2024, 04, 20, 12, 30, 0, TimeSpan.Zero), "Test news 2"));
        await MyBackendClient.Admin.AddNewsAsync(new NewsItem(new DateTimeOffset(2024, 04, 20, 12, 30, 0, TimeSpan.Zero), "Test news 3"));

        var years = await MyBackendClient.News.GetYearsAsync();
        Assert.That(years, Is.EquivalentTo(Years));

        var news = await MyBackendClient.News.GetNewsAsync(2023);
        Assert.That(news, Has.Length.GreaterThanOrEqualTo(1));

        Assert.Multiple(() =>
        {
            Assert.That(news[0].DateTime, Is.EqualTo(offset1));
            Assert.That(news[0].Text, Is.EqualTo("Test news"));
        });
    }
}
