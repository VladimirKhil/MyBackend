using MyBackend.Service.Contract.Models;
using MyBackend.Service.Contract.Request;
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

        app.MapPost("/api/v1/admin/tags", async (IBlogsService blogsService, TagCreateRequest tagCreateRequest, CancellationToken cancellationToken) =>
        {
            var tagId = await blogsService.AddTagAsync(tagCreateRequest.Value, cancellationToken);
            return Results.Created($"/api/v1/tags/{tagId}", tagId);
        });

        app.MapPost("/api/v1/admin/blogs", async (IBlogsService blogsService, BlogEntryCreateRequest blogEntryCreateRequest, CancellationToken cancellationToken) =>
        {
            var entryId = await blogsService.AddEntryAsync(blogEntryCreateRequest, cancellationToken);
            return Results.Created($"/api/v1/blogs/{entryId}", entryId);
        });
    }
}
