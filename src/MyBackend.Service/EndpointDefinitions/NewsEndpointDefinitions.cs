using Microsoft.AspNetCore.Mvc;
using MyBackend.Service.Contract.Response;
using MyBackend.Service.Contracts;
using MyBackend.Service.Helpers;
using MyBackend.Service.Models;

namespace MyBackend.Service.EndpointDefinitions;

public static class NewsEndpointDefinitions
{
    public static void DefineNewsEndpoint(WebApplication app)
    {
        app.MapGet("api/v1/news/years", async (INewsService newsService, CancellationToken cancellationToken) =>
        {
            var years = await newsService.GetYearsAsync(cancellationToken);
            return Results.Ok(new YearsResponse(years));
        });

        app.MapGet("api/v1/news/years/{year}", async (
            INewsService newsService,
            int year,
            [FromHeader(Name = "Accept-Language")] string acceptLanguage = Constants.DefaultCultureCode,
            CancellationToken cancellationToken = default) =>
        {
            var culture = CultureHelper.GetCultureFromAcceptLanguageHeader(acceptLanguage);
            var newsItems = await newsService.GetNewsAsync(year, culture, cancellationToken);
            return Results.Ok(new NewsResponse(newsItems));
        });
    }
}
