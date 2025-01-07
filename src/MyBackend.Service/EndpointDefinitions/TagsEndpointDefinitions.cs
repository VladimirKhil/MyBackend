using Microsoft.AspNetCore.Mvc;
using MyBackend.Service.Contract.Response;
using MyBackend.Service.Contracts;
using MyBackend.Service.Helpers;
using MyBackend.Service.Models;

namespace MyBackend.Service.EndpointDefinitions;

public static class TagsEndpointDefinitions
{
    public static void DefineTagsEndpoints(WebApplication app)
    {
        app.MapGet("/api/v1/tags", async (
            IBlogsService blogsService,
            [FromHeader(Name = "Accept-Language")] string acceptLanguage = Constants.DefaultCultureCode,
            CancellationToken cancellationToken = default) =>
        {
            var culture = CultureHelper.GetCultureFromAcceptLanguageHeader(acceptLanguage);
            var tags = await blogsService.GetTagsAsync(culture, cancellationToken);
            return Results.Ok(new TagsResponse(tags));
        });
    }
}
