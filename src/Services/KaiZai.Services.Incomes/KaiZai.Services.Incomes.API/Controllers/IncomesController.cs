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
        _logger = logger;
        _iIncomeService = iIncomeService;
    }

    // GET: api/profile/{profileId}/incomes/{id}
    [HttpGet("{id}")]
    public ActionResult<IncomeDTO> GetIncome(Guid id)
    {
        // Implement code to retrieve a specific income record by ID
        var income = _iIncomeService.FirstOrDefault(i => i.Id == id);
        if (income == null)
        {
            return NotFound();
        }
        return income;
    }
    // POST: api/incomes
    [HttpPost]
    public ActionResult<Income> CreateIncome([FromBody] Income income)
    {
        // Implement code to create a new income record
        income.Id = Guid.NewGuid(); // Generate a new ID
        _incomes.Add(income);
        return CreatedAtAction(nameof(GetIncome), new { id = income.Id }, income);
    }
    // PUT: api/incomes/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateIncome(Guid id, [FromBody] Income updatedIncome)
    {
        // Implement code to update an existing income record
        var income = _incomes.FirstOrDefault(i => i.Id == id);
        if (income == null)
        {
            return NotFound();
        }
        // Update income properties with data from updatedIncome
        // Example: income.Property = updatedIncome.Property;
        return NoContent();
    }
    // DELETE: api/incomes/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteIncome(Guid id)
    {
        // Implement code to delete an income record by ID
        var income = _incomes.FirstOrDefault(i => i.Id == id);
        if (income == null)
        {
            return NotFound();
        }
        _incomes.Remove(income);
        return NoContent();
    }
}
