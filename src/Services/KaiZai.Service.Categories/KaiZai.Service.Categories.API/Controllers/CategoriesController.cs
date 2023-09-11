using KaiZai.Service.Categories.API.Common;
using KaiZai.Service.Categories.API.Data.Entities;
using KaiZai.Service.Categories.API.Data.Repositories;
using KaiZai.Service.Categories.API.DTOs;
using KaiZai.Service.Categories.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace KaiZai.Service.Categories.API.Controllers;

[ApiController]
[Route("api/profile/{profileId}/[controller]")]
public sealed class CategoriesController : ControllerBase
{
    private readonly ILogger<CategoriesController> _logger;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public CategoriesController(ILogger<CategoriesController> logger, 
        ICategoryRepository categoryRepository,
        IPublishEndpoint publishEndpoint)
    {
        //TODO: later implement logging through serilog
        _logger = logger;
        _categoryRepository = categoryRepository;
        _publishEndpoint = publishEndpoint;
    }

    [Route("{id}")]
    [HttpGet]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryDTO>> GetCategoryOfProfileById(Guid profileId, Guid id)
    {
        var category = await _categoryRepository.GetOneAsync(x => x.ProfileId == profileId && x.Id == id);
        if (category == null)
        {
            return NotFound();
        }

        var categoryDTO = category.ToCategoryDTO();
        return Ok(categoryDTO);
    }

    [HttpPost]
    [ProducesResponseType( StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateCategory(Guid profileId, [FromBody] CreateCategoryDTO createCategoryDTO)
    {
        var categoryItem = createCategoryDTO.ToCategory(profileId);
        await _categoryRepository.CreateAsync(categoryItem);
        
        if (categoryItem.Id == Guid.Empty)
        {
            
        }

        await _publishEndpoint.Publish(new CategoryCreated(categoryItem.Id, categoryItem.ProfileId, categoryItem.Name, categoryItem.CategoryType));
        return CreatedAtAction(nameof(GetCategoryOfProfileById), new { id = categoryItem.Id }, null);
    }

}
