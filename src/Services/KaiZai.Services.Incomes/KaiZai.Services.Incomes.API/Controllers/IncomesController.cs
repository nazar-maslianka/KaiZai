using KaiZai.Services.Incomes.BAL.Core;
using KaiZai.Services.Incomes.BAL.DTOs;
using KaiZai.Services.Incomes.BAL.Services;
using Microsoft.AspNetCore.Mvc;

namespace KaiZai.Services.Incomes.API.Controllers;

[ApiController]
[Route("api/profile/{profileId}/[controller]")]
public class IncomesController : ControllerBase
{
    private readonly ILogger<IncomesController> _logger;
    private readonly IIncomeService _iIncomeService;
    public IncomesController(ILogger<IncomesController> logger,
        IIncomeService iIncomeService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _iIncomeService = iIncomeService ?? throw new ArgumentNullException(nameof(iIncomeService));
    }

    #region Get operations
    // GET: api/profile/{profileId}/incomes/{id}
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IncomeDTO>> GetIncome(Guid id)
    {
        // Implement code to retrieve a specific income record by ID
        var result = await _iIncomeService.GetIncomeByIdAsync(id);
        if (result.ProcessStatus == ProcessStatus.UserError)
        {
            _logger.LogWarning(result.UserError);
            return NotFound();
        }

        return Ok(result.Value);
    }

    // GET: api/profile/{profileId}/incomes?profileId=12345&pageNumber=1&pageSize=10&startDate=2023-01-01T00:00:00&endDate=2023-06-30T23:59:59
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PagedList<IncomeShortDTO>>> GetIncomesAggregatedByPageAsync(Guid profileId,
        [FromQuery] PagingParams pagingParams,
        [FromQuery] FilteringParams? filteringParams = null)
    {
        if (pagingParams == null)
        {
            return BadRequest("Paging parameters are required.");
        }

        var result = await _iIncomeService
            .GetIncomesAggregatedByPageAsync(profileId, pagingParams, filteringParams);

        if (result.ProcessStatus == ProcessStatus.UserError)
        {
            _logger.LogWarning(result.UserError);
            return NotFound();
        }
        
        if (result.ProcessStatus == ProcessStatus.SystemError)
        {
            _logger.LogWarning(result.SystemError);
            return !string.IsNullOrWhiteSpace(result.UserError) 
                ? StatusCode(500, result.UserError) 
                : StatusCode(500);
        }

        return Ok(result.Value);
    }
    #endregion

    #region CRUD operations
    // POST: api/profile/{profileId}/incomes
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> AddIncome(Guid profileId,
        [FromBody] AddUpdateIncomeDTO addIncome)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        var result = await _iIncomeService.AddIncomeAsync(profileId, addIncome);
        
        if (result.ProcessStatus == ProcessStatus.UserError)
        {
            _logger.LogWarning(result.UserError);
            return BadRequest();
        }

        return NoContent();
    }

    // PUT: api/profile/{profileId}/incomes/{id}
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateIncome(Guid profileId,
        Guid id, 
        [FromBody] AddUpdateIncomeDTO updatedIncome)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        var result = await _iIncomeService.UpdateIncomeAsync(profileId, id, updatedIncome);
        
        if (result.ProcessStatus == ProcessStatus.UserError)
        {
            _logger.LogWarning(result.UserError);
            return Conflict();
        }

        return NoContent();
    }
    // DELETE: api/profile/{profileId}/incomes/{id}
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteIncome(Guid id)
    {
        var income = await _iIncomeService.GetIncomeByIdAsync(id);
        if (income == null)
        {
            return NotFound();
        }

        var result = await _iIncomeService.DeleteIncomeAsync(id);
        return NoContent();
    }
    #endregion
}
