using Microsoft.AspNetCore.Mvc;
using MyBackend.Service.Contract.Models;
using MyBackend.Service.Contracts;

namespace MyBackend.Service.Controllers;

[Route("api/v1/admin")]
[ApiController]
public sealed class AdminController(INewsService newsService) : ControllerBase
{
    [HttpPost("news")]
    public async Task<IActionResult> AddNewsAsync(NewsItem newsItem, CancellationToken cancellationToken)
    {
        await newsService.AddNewsAsync(newsItem, cancellationToken);
        return Accepted();
    }
}
