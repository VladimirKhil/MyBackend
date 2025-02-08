using Microsoft.AspNetCore.Mvc;
using MyBackend.Service.Contracts;
using MyBackend.Service.Helpers;
using MyBackend.Service.Models;
using System.ComponentModel.DataAnnotations;

namespace MyBackend.Service.EndpointDefinitions;

public static class BlogsEndpointDefinitions
{
    public static void DefineBlogsEndpoints(WebApplication app)
    {
        app.MapGet("/api/v1/blogs", async (
            IBlogsService blogsService,
            int? tagId = null,
            [FromHeader(Name = "Accept-Language")] string acceptLanguage = Constants.DefaultCultureCode,
            [Range(0, int.MaxValue)]
            int from = 0,
            [Range(1, 100)]
            int count = 10,
            CancellationToken cancellationToken = default) =>
        {
            var culture = CultureHelper.GetCultureFromAcceptLanguageHeader(acceptLanguage);
            var entriesPage = await blogsService.GetEntriesAsync(culture, tagId, from, count, cancellationToken);
            return Results.Ok(entriesPage);
        });

        app.MapGet("/api/v1/blogs/{id}", async (
            IBlogsService blogsService,
            int id,
            [FromHeader(Name = "Accept-Language")] string acceptLanguage = Constants.DefaultCultureCode,
            CancellationToken cancellationToken = default) =>
        {
            var culture = CultureHelper.GetCultureFromAcceptLanguageHeader(acceptLanguage);
            var entry = await blogsService.GetEntryAsync(id, culture, cancellationToken);
            return Results.Ok(entry);
        });
    }
}
