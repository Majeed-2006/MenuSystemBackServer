using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using BusinessLayer.DTOClasses.Categories;
using BusinessLayer.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _CategoryService;

        public CategoryController(CategoryService CategoryService)
        {
            _CategoryService = CategoryService;
        }

        [HttpGet("All", Name = "GetAllCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllCategories()
        {
            List<CategoryDTO> CategoriesList =
                await _CategoryService.GetAllCategoriesAsync();

            if (CategoriesList == null || CategoriesList.Count == 0)
                return NotFound("No categories found.");

            return Ok(CategoriesList);
        }

        [HttpGet("{id}", Name = "FindCategoryByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDTO>> FindCategoryByID(int id)
        {
            if (id < 1)
                return BadRequest("Category ID must be greater than 0.");

            CategoryDTO CategoryDTO =
                await _CategoryService.FindAsync(id);

            if (CategoryDTO == null)
                return NotFound($"Category with ID {id} was not found.");

            return Ok(CategoryDTO);
        }

        [HttpPost(Name = "PostAddCategory")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CategoryDTO>> AddCategory(
            CategorySaveDTO newCategory)
        {
            if (newCategory == null)
                return BadRequest("Category data cannot be empty.");

            if (string.IsNullOrWhiteSpace(newCategory.Name))
                return BadRequest("Category name is required.");

            if (newCategory.PrepDuration < 0)
                return BadRequest("Preparation duration must be greater than or equal to 0.");

            if (newCategory.RestaurantId < 1)
                return BadRequest("Restaurant ID must be greater than 0.");

            if (newCategory.CreatedByUserId < 1)
                return BadRequest("Created By User ID must be greater than 0.");

            int id = await _CategoryService.AddAsync(newCategory);

            if (id != -1)
            {
                CategoryDTO CategoryDTO = new CategoryDTO(
                    id,
                    newCategory.Name,
                    newCategory.Discription,
                    newCategory.IsAvilable,
                    newCategory.PrepDuration,
                    newCategory.RestaurantId,
                    newCategory.CreatedByUserId
                );

                return CreatedAtRoute(
                    "FindCategoryByID",
                    new { Id = id },
                    CategoryDTO
                );
            }

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                "An error occurred while adding the category."
            );
        }

        [HttpPatch("{id}", Name = "PatchCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CategorySaveDTO>> PatchCategory(
            int id,
            [FromBody] JsonPatchDocument<CategorySaveDTO> patchDoc)
        {
            if (id < 1)
                return BadRequest("Category ID must be greater than 0.");

            if (patchDoc == null)
                return BadRequest("Invalid or empty patch document.");

            CategorySaveDTO category =
                await _CategoryService.FindSaveAsync(id);

            if (category == null)
                return NotFound($"Category with ID {id} was not found.");

            patchDoc.ApplyTo(category, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(category.Name))
                return BadRequest("Category name cannot be empty.");

            if (category.PrepDuration < 0)
                return BadRequest(
                    "Preparation duration must be greater than or equal to 0."
                );

            if (category.RestaurantId < 1)
                return BadRequest(
                    $"Invalid Restaurant ID: {category.RestaurantId}."
                );

            if (category.CreatedByUserId < 1)
                return BadRequest(
                    $"Invalid Created By User ID: {category.CreatedByUserId}."
                );

            if (await _CategoryService.UpdateAsync(id, category))
                return Ok(category);
            else
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "An error occurred while updating the category."
                );
        }

        [HttpPut("{id}", Name = "UpdateCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CategorySaveDTO>> UpdateCategory(
            int id,
            CategorySaveDTO updatedCategory)
        {
            if (id < 1)
                return BadRequest("Category ID must be greater than 0.");

            if (updatedCategory == null)
                return BadRequest("Updated category data cannot be empty.");

            if (string.IsNullOrWhiteSpace(updatedCategory.Name))
                return BadRequest("Category name is required.");

            if (updatedCategory.PrepDuration < 0)
                return BadRequest(
                    "Preparation duration must be greater than or equal to 0."
                );

            if (updatedCategory.RestaurantId < 1)
                return BadRequest(
                    $"Invalid Restaurant ID: {updatedCategory.RestaurantId}."
                );

            if (updatedCategory.CreatedByUserId < 1)
                return BadRequest(
                    $"Invalid Created By User ID: {updatedCategory.CreatedByUserId}."
                );

            if (!await _CategoryService.IsExistAsync(id))
                return NotFound($"Category with ID {id} was not found.");

            if (await _CategoryService.UpdateAsync(id, updatedCategory))
                return Ok(updatedCategory);
            else
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "An error occurred while updating the category."
                );
        }

        [HttpDelete("{id}", Name = "DeleteCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
       
        public async Task<ActionResult> DeleteCategory(int id)
        {
            if (id < 1)
                return BadRequest("Category ID must be greater than 0.");

            if (!await _CategoryService.IsExistAsync(id))
                return NotFound($"Category with ID {id} was not found.");

            if (await _CategoryService.DeleteAsync(id))
                return Ok("Category was deleted successfully.");
            else
                return Conflict("Cannot delete this category because it is linked to other entities.");
        }
    }
}
