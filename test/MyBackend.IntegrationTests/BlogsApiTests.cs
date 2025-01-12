using MyBackend.Service.Contract.Request;

namespace MyBackend.IntegrationTests;

internal sealed class BlogsApiTests : TestsBase
{
    [Test]
    public async Task AddTag_Ok()
    {
        var tag1Value = "My tag" + Guid.NewGuid().ToString("N");
        var tag2Value = "My tag" + Guid.NewGuid().ToString("N");
        var tag3Value = "My tag" + Guid.NewGuid().ToString("N");

        await MyBackendClient.Admin.AddTagAsync(tag1Value);
        await MyBackendClient.Admin.AddTagAsync(tag2Value);
        await MyBackendClient.Admin.AddTagAsync(tag3Value);
        var tags = await MyBackendClient.Tags.GetTagsAsync();

        Assert.That(tags, Has.Length.GreaterThanOrEqualTo(3));

        tags = [.. tags.OrderBy(tag => tag.Value)];

        Assert.Multiple(() =>
        {
            Assert.That(tags.Any(tag => tag.Value == tag1Value));
            Assert.That(tags.Any(tag => tag.Value == tag2Value));
            Assert.That(tags.Any(tag => tag.Value == tag3Value));
        });
    }

    [Test]
    public async Task AddBlog_Ok()
    {
        var offset1 = new DateTimeOffset(2023, 04, 20, 12, 30, 0, TimeSpan.Zero);
        var offset3 = new DateTimeOffset(2024, 04, 20, 12, 30, 0, TimeSpan.Zero);
        var tagValue = "My tag" + Guid.NewGuid().ToString("N");
        var tagId = await MyBackendClient.Admin.AddTagAsync(tagValue);

        await MyBackendClient.Admin.AddEntryAsync(new BlogEntryCreateRequest(offset1, "Test blog", "Text", []));

        await MyBackendClient.Admin.AddEntryAsync(new BlogEntryCreateRequest(
            new DateTimeOffset(2024, 04, 20, 12, 30, 0, TimeSpan.Zero),
            "Test blog 2",
            "Text",
            []));

        var entryId3 = await MyBackendClient.Admin.AddEntryAsync(new BlogEntryCreateRequest(
            offset3,
            "Test blog 3",
            "Text",
            [tagId]));

        var entriesByTagPage = await MyBackendClient.Blogs.GetBlogEntriesAsync(tagId: tagId);
        Assert.That(entriesByTagPage.TotalCount, Is.EqualTo(1));

        var entriesByTag = entriesByTagPage.Entries;
        Assert.That(entriesByTag, Has.Length.EqualTo(1));

        var entry = entriesByTag[0];
        Assert.That(entry.Tags, Has.Length.EqualTo(1));

        Assert.Multiple(() =>
        {
            Assert.That(entry.Tags[0].Id, Is.EqualTo(tagId));
            Assert.That(entry.Tags[0].Value, Is.EqualTo(tagValue));
            Assert.That(entry.Id, Is.EqualTo(entryId3));
            Assert.That(entry.DateTime, Is.EqualTo(offset3));
            Assert.That(entry.Text, Is.EqualTo("Text"));
            Assert.That(entry.Title, Is.EqualTo("Test blog 3"));
        });

        var entries = await MyBackendClient.Blogs.GetBlogEntriesAsync();
        Assert.That(entries.TotalCount, Is.GreaterThanOrEqualTo(3));
    }
}
