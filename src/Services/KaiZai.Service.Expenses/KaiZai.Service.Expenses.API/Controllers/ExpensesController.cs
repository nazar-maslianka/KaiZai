using KaiZai.Service.Expenses.BAL.Core;
using KaiZai.Service.Expenses.BAL.DTOs;
using KaiZai.Service.Expenses.BAL.Services;
using Microsoft.AspNetCore.Mvc;

namespace KaiZai.Service.Expenses.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly ILogger<ExpensesController> _logger;
    private readonly IExpenseService _iExpenseService;
    public ExpensesController(ILogger<ExpensesController> logger,
        IExpenseService iExpenseService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _iExpenseService = iExpenseService ?? throw new ArgumentNullException(nameof(iExpenseService));
    }

    #region Get operations
    // GET: api/profile/{profileId}/expenses/{id}
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExpenseDTO>> GetExpense(Guid id)
    {
        // Implement code to retrieve a specific expense record by ID
        var result = await _iExpenseService.GetExpenseByIdAsync(id);
        if (result.ProcessStatus == ProcessStatus.UserError)
        {
            _logger.LogWarning(result.UserError);
            return NotFound();
        }

        return Ok(result.Value);
    }

    // GET: api/profile/{profileId}/expenses?profileId=12345&pageNumber=1&pageSize=10&startDate=2023-01-01T00:00:00&endDate=2023-06-30T23:59:59
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PagedList<ExpenseShortDTO>>> GetExpensesAggregatedByPageAsync(Guid profileId,
        [FromQuery] PagingParams pagingParams,
        [FromQuery] FilteringParams? filteringParams = null)
    {
        if (pagingParams == null)
        {
            return BadRequest("Paging parameters are required.");
        }

        var result = await _iExpenseService
            .GetExpensesAggregatedByPageAsync(profileId, pagingParams, filteringParams);

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
    // POST: api/profile/{profileId}/expenses
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> AddExpense(Guid profileId,
        [FromBody] AddUpdateExpenseDTO addExpense)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        var result = await _iExpenseService.AddExpenseAsync(profileId, addExpense);
        
        if (result.ProcessStatus == ProcessStatus.UserError)
        {
            _logger.LogWarning(result.UserError);
            return BadRequest();
        }

        return NoContent();
    }

    // PUT: api/profile/{profileId}/expenses/{id}
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateExpense(Guid profileId,
        Guid id, 
        [FromBody] AddUpdateExpenseDTO updatedExpense)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        var result = await _iExpenseService.UpdateExpenseAsync(profileId, id, updatedExpense);
        
        if (result.ProcessStatus == ProcessStatus.UserError)
        {
            _logger.LogWarning(result.UserError);
            return Conflict();
        }

        return NoContent();
    }
    // DELETE: api/profile/{profileId}/expenses/{id}
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteExpense(Guid id)
    {
        var expense = await _iExpenseService.GetExpenseByIdAsync(id);
        if (expense == null)
        {
            return NotFound();
        }

        var result = await _iExpenseService.DeleteExpenseAsync(id);
        return NoContent();
    }
    #endregion
}
