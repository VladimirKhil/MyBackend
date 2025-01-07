using MyBackend.Service.Contract.Models;
using MyBackend.Service.Contracts;

namespace MyBackend.Service.EndpointDefinitions;

public static class AdminEndpointDefinitions
{
    public static void DefineAdminEndpoint(WebApplication app)
    {
        app.MapPost("/api/v1/admin/news", async (INewsService newsService, NewsItem newsItem, CancellationToken cancellationToken) =>
        {
            await newsService.AddNewsAsync(newsItem, cancellationToken);
            return Results.Accepted();
        });
    }
}


