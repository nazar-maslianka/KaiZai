using KaiZai.Service.Categories.API.Common;
using KaiZai.Service.Categories.API.Data.Entities;
using KaiZai.Service.Categories.API.Data.Repositories;
using KaiZai.Service.Categories.API.DTOs;
using KaiZai.Service.Categories.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace KaiZai.Service.Categories.API.Controllers;

//TODO: check all methods states and possible situations later!
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
        _logger = logger;
        _categoryRepository = categoryRepository;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesOfProfile(Guid profileId)
    {
        var categories = await _categoryRepository
            .GetAllAsync(categ => categ.ProfileId == profileId);

        var categoriesDTO = categories
            .Select(c => c.ToCategoryDTO());
        return Ok(categoriesDTO);
    }

    [Route("{id}")]
    [HttpGet]
    [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryDTO>> GetCategoryOfProfileById(Guid profileId, Guid id)
    {
        var category = await _categoryRepository.GetOneAsync(categ => categ.ProfileId == profileId && categ.Id == id);
        if (category == null)
        {
            _logger.LogInformation("Category with id:{@id} for profile:{@profileId} not found in the database",
                id, profileId);
            
            return NotFound();
        }

        var categoryDTO = category.ToCategoryDTO();
        return Ok(categoryDTO);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateCategory(Guid profileId, [FromBody] CreateUpdateCategoryDTO createCategoryDTO)
    {
        if (createCategoryDTO == null)
        {
            _logger.LogError("Object: @{createCategoryDTO} sent from client is null.", createCategoryDTO);
            return BadRequest("CreateUpdateCategoryDTO object is null");
        }
        if (!ModelState.IsValid)
        {
            _logger.LogError("Invalid model state for the CreateUpdateCategoryDTO object");
            return UnprocessableEntity(ModelState);
        }

        var category = createCategoryDTO.ToCategory(profileId);
        await _categoryRepository.CreateAsync(category);
        
        if (category.Id == Guid.Empty)
        {
            _logger.LogError("Category not created");
            
            return BadRequest();
        }

        await _publishEndpoint.Publish(new CategoryCreated(category.Id, category.ProfileId, category.Name, category.CategoryType));
        return NoContent();
    }

    [Route("{id}")]
    [HttpPut]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateCategory(Guid profileId, Guid id, [FromBody] CreateUpdateCategoryDTO updateCategoryDTO)
    {
        if (updateCategoryDTO == null)
        {
            _logger.LogError("Object: @{updateCategoryDTO} sent from client is null.", updateCategoryDTO);
            return BadRequest("CreateUpdateCategoryDTO object is null");
        }
        if (!ModelState.IsValid)
        {
            _logger.LogError("Invalid model state for the CreateUpdateCategoryDTO object");
            return UnprocessableEntity(ModelState);
        }

        var category = updateCategoryDTO.ToCategory(profileId, id);

        await _categoryRepository.UpdateAsync(category);

        await _publishEndpoint.Publish(new CategoryUpdated(category.Id, category.ProfileId,  category.Name, category.CategoryType));
        return NoContent();
    }

    [Route("{id}")]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCategory(Guid id, Guid profileId)
    {
        var category = await _categoryRepository.GetOneAsync(id);
        if (category == null || category.ProfileId != profileId)
        {
              _logger.LogInformation("Category with id:{@id} for profile:{@profileId} not found in the database",
                id, profileId);
            
            return NotFound();
        }
        await _categoryRepository.RemoveAsync(id);
        await _publishEndpoint.Publish(new CategoryDeleted(id, profileId, category.CategoryType));
        return NoContent();
    }
}
