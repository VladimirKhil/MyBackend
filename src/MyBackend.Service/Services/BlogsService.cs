using LinqToDB;
using MyBackend.Database;
using MyBackend.Service.Contract.Models;
using MyBackend.Service.Contract.Request;
using MyBackend.Service.Contracts;

namespace MyBackend.Service.Services;

public sealed class BlogsService(MyBackendConnection connection) : IBlogsService
{
    public Task<int> AddTagAsync(string tag, CancellationToken cancellationToken = default) =>
        connection.Tags.InsertWithInt32IdentityAsync(() => new Database.Models.TagsModel { Value = tag }, cancellationToken);

    public Task<Tag[]> GetTagsAsync(string? culture = null, CancellationToken cancellationToken = default) =>
        connection.Tags.Select(tag => new Tag(tag.Id, tag.Value)).ToArrayAsync(cancellationToken);

    public async Task<int> AddEntryAsync(BlogEntryCreateRequest entry, CancellationToken cancellationToken = default)
    {
        using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        var entryId = await connection.BlogEntries.InsertWithInt32IdentityAsync(
            () => new Database.Models.BlogEntriesModel
            {
                DateTime = entry.DateTime,
                Title = entry.Title,
                Text = entry.Text
            },
            cancellationToken);

        foreach (var tagId in entry.Tags)
        {
            await connection.BlogEntriesTags.InsertAsync(
                () => new Database.Models.BlogEntryTag { BlogEntryId = entryId, TagId = tagId },
                cancellationToken);
        }

        transaction.Commit();

        return entryId;
    }

    public async Task<BlogEntriesPage> GetEntriesAsync(
        string? culture = null,
        int? tagId = null,
        int from = 0,
        int count = 10,
        CancellationToken cancellationToken = default)
    {
        var query = from entry in connection.BlogEntries
                    join entryTag in connection.BlogEntriesTags on entry.Id equals entryTag.BlogEntryId
                    join tag in connection.Tags on entryTag.TagId equals tag.Id into tags
                    where tagId == null || tags.Any(tag => tag.Id == tagId)
                    orderby entry.DateTime descending
                    select new BlogEntry(
                        entry.Id,
                        entry.DateTime,
                        entry.Title,
                        entry.Text,
                        tags.DefaultIfEmpty().Select(tag => new Tag(tag!.Id, tag!.Value)).ToArray());

        var entries = await query.Skip(from).Take(count).ToArrayAsync(cancellationToken);
        var total = await query.CountAsync(cancellationToken);

        return new BlogEntriesPage { Entries = entries, TotalCount = total };
    }
}
