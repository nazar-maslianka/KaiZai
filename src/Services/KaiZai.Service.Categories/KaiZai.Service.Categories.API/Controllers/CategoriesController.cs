using Microsoft.AspNetCore.Mvc;

namespace KaiZai.Service.Categories.API.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class CategoriesController : ControllerBase
{
    private readonly ILogger<CategoriesController> _logger;

    public CategoriesController(ILogger<CategoriesController> logger)
    {
        _logger = logger;
    }
}
