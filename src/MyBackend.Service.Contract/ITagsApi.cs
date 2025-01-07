using MyBackend.Service.Contract.Models;

namespace MyBackend.Service.Contract;

public interface ITagsApi
{
    Task<Tag[]> GetTagsAsync(CancellationToken cancellationToken = default);
}
