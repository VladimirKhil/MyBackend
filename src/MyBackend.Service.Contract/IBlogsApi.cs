using MyBackend.Service.Contract.Models;

namespace MyBackend.Service.Contract;

public interface IBlogsApi
{
    Task<BlogEntriesPage> GetBlogEntriesAsync(string? tag = null, int from = 0, int count = 10, CancellationToken cancellationToken = default);
}
