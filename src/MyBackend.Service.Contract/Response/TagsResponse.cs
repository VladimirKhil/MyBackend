using MyBackend.Service.Contract.Models;

namespace MyBackend.Service.Contract.Response;

/// <summary>
/// Defines a tags response model.
/// </summary>
/// <param name="Tags">Array of tags.</param>
public sealed record TagsResponse(Tag[] Tags);
