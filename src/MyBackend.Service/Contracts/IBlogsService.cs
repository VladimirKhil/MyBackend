using MyBackend.Service.Contract.Models;
using MyBackend.Service.Contract.Request;

namespace MyBackend.Service.Contracts;

public interface IBlogsService
{
    Task<int> AddTagAsync(string tag, CancellationToken cancellationToken = default);

    Task<Tag[]> GetTagsAsync(string? culture = null, CancellationToken cancellationToken = default);

    Task<int> AddEntryAsync(BlogEntryCreateRequest entry, CancellationToken cancellationToken = default);

    Task<BlogEntriesPage> GetEntriesAsync(
        string? culture = null,
        int? tagId = null,
        int from = 0,
        int count = 10,
        CancellationToken cancellationToken = default);

    Task<BlogEntry> GetEntryAsync(int id, string? culture = null, CancellationToken cancellationToken = default);
}
