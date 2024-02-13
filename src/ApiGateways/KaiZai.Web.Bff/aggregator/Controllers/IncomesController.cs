using System.Text.Json;
using KaiZai.Web.HttpAggregator.Core;
using KaiZai.Web.HttpAggregator.Models;
using KaiZai.Web.HttpAggregator.Services;
using Microsoft.AspNetCore.Mvc;

namespace KaiZai.Web.HttpAggregator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncomesController : ControllerBase
{
    private readonly Guid profileIdC = new Guid("a476e83e-3ecc-4708-8880-af88c4dd04da");
    private readonly IIncomeService _iIncomes;
    public IncomesController(
        IIncomeService iIncomes)
    {
        _iIncomes = iIncomes ?? throw new ArgumentNullException(nameof(iIncomes));
    }

    #region Get operations
    // GET: api/incomes/{id}
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IncomeDataItem>> GetIncome(Guid id)
    {
        // Implement code to retrieve a specific income record by ID
        var result = await _iIncomes.GetIncomeById(id);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    // GET: api/incomes?pageNumber=1&pageSize=10&startDate=2023-01-01T00:00:00&endDate=2023-06-30T23:59:59
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagedDataItemsList<IncomeDataItem>>> GetIncomesAggregatedByPageAsync(
        [FromHeader] Guid ProfileId,
        [FromQuery] PagingParams pagingParams,
        [FromQuery] FilteringParams filteringParams = null)
    {
        if (pagingParams == null)
        {
            return BadRequest("Paging parameters are required.");
        }

        var pagedIncomeDataItemsList = await _iIncomes
        .GetPagedIncomes(new GetPagedIncomesRequest(
            ProfileId, 
            pagingParams, 
            filteringParams ?? new FilteringParams()));

        Response.Headers.Add("Metadata", 
            JsonSerializer.Serialize(pagedIncomeDataItemsList.Metadata));

        return Ok(pagedIncomeDataItemsList);
    }
    #endregion

    #region CRUD operations
    // POST: api/incomes
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> AddIncome(
        [FromHeader] Guid profileId,
        [FromBody] AddUpdateIncomeRequest addIncome)
    {
        var response = await _iIncomes.AddIncome(new AddUpdateIncomeRequest(
            profileId,
            addIncome.CategoryId,
            addIncome.IncomeDate,
            addIncome.Description,
            addIncome.Amount));     
        
        return NoContent();
    }

    // // PUT: api/incomes/{id}
    // [HttpPut("{id}")]
    // [ProducesResponseType(StatusCodes.Status204NoContent)]
    // [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    // [ProducesResponseType(StatusCodes.Status409Conflict)]
    // public async Task<IActionResult> UpdateIncome(Guid profileId,
    //     Guid id, 
    //     [FromBody] AddUpdateIncomeDTO updatedIncome)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return UnprocessableEntity(ModelState);
    //     }

    //     var result = await _iIncomes.UpdateIncomeAsync(profileId, id, updatedIncome);
        
    //     if (result.ProcessStatus == ProcessStatus.UserError)
    //     {
    //         _logger.LogWarning(result.UserError);
    //         return Conflict();
    //     }

    //     return NoContent();
    // }
    
    // // DELETE: api/incomes/{id}
    // [HttpDelete("{id}")]
    // [ProducesResponseType(StatusCodes.Status204NoContent)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public async Task<IActionResult> DeleteIncome(Guid id)
    // {
    //     var income = await _iIncomes.GetIncomeByIdAsync(id);
    //     if (income == null)
    //     {
    //         return NotFound();
    //     }

    //     var result = await _iIncomes.DeleteIncomeAsync(id);
    //     return NoContent();
    // }
    #endregion
}
