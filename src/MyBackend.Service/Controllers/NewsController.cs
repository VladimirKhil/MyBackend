using Microsoft.AspNetCore.Mvc;
using MyBackend.Service.Contract.Response;
using MyBackend.Service.Contracts;

namespace MyBackend.Service.Controllers;

[Route("api/v1/news")]
[ApiController]
public sealed class NewsController(INewsService newsService) : ControllerBase
{
    [HttpGet("years")]
    public async Task<IActionResult> GetYearsAsync(CancellationToken cancellationToken)
    {
        var years = await newsService.GetYearsAsync(cancellationToken);
        return Ok(new YearsResponse(years));
    }

    [HttpGet("years/{year}")]
    public async Task<IActionResult> GetNewsAsync(int year, CancellationToken cancellationToken)
    {
        var newsItems = await newsService.GetNewsAsync(year, cancellationToken);
        return Ok(new NewsResponse(newsItems));
    }
}
