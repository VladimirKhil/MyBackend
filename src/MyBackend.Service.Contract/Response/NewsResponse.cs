using MyBackend.Service.Contract.Models;

namespace MyBackend.Service.Contract.Response;

/// <summary>
/// Defines a news response.
/// </summary>
/// <param name="News">Array of news.</param>
public sealed record NewsResponse(NewsItem[] News);
