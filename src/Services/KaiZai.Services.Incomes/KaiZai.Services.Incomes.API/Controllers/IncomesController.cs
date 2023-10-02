using Microsoft.AspNetCore.Mvc;

namespace KaiZai.Services.Incomes.API.Controllers;

[ApiController]
[Route("api/profile/{profileId}/[controller]")]
public class IncomesController : ControllerBase
{
    public IncomesController()
    {
    }
}
