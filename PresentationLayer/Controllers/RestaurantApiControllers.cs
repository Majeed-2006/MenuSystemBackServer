using BusinessLayer.DTOClasses.Restaurants;
using BusinessLayer.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using static App.API.GloabalClasses.Validation;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantService _RestaurantService;

        public RestaurantController(RestaurantService RestaurantService)
        {
            _RestaurantService = RestaurantService;
        }

        [HttpGet("All", Name = "GetAllRestaurants")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<RestaurantDTO>>> GetAllRestaurants()
        {
            List<RestaurantDTO> RestaurantsList = await _RestaurantService.GetAllRestaurantsAsync();
            if (RestaurantsList == null || RestaurantsList.Count == 0)
                return NotFound("No restaurants found.");

            return Ok(RestaurantsList);
        }


        [HttpGet("{id}", Name = "FindRestaurantByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RestaurantDTO>> FindRestaurantByID(int id)
        {
            if (id < 1)
                return BadRequest("Restaurant ID must be greater than 0.");

            RestaurantDTO RestaurantDTO = await _RestaurantService.FindAsync(id);
            if (RestaurantDTO == null)
                return NotFound($"Restaurant with ID {id} was not found.");

            return Ok(RestaurantDTO);
        }

        [HttpPost(Name = "PostAddRestaurant")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RestaurantDTO>> AddRestaurant(RestaurantSaveDTO newRestaurant)
        {
            if (newRestaurant == null)
                return BadRequest("Restaurant data cannot be empty.");

            if (string.IsNullOrWhiteSpace(newRestaurant.Name))
                return BadRequest("Restaurant name is required.");

            if (newRestaurant.CreatedAt > DateTime.Now)
                return BadRequest("Creation date cannot be in the future.");

            if (!ValidateSubDomain(newRestaurant.SubDomain))
                return BadRequest("Invalid sub-domain format.");

            int id = await _RestaurantService.AddAsync(newRestaurant);
            if (id != -1)
            {
                RestaurantDTO RestaurantDTO = new RestaurantDTO(
                    id,
                    newRestaurant.Name,
                    newRestaurant.SubDomain,
                    newRestaurant.CreatedAt,
                    newRestaurant.IsDeleted
                );
                return CreatedAtRoute("FindRestaurantByID", new { Id = id }, RestaurantDTO);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the restaurant.");
        }


        [HttpPatch("{id}", Name = "PatchRestaurant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RestaurantSaveDTO>> PatchRestaurant(int id, [FromBody] JsonPatchDocument<RestaurantSaveDTO> patchDoc)
        {
            if (id < 1)
                return BadRequest("Restaurant ID must be greater than 0.");

            if (patchDoc == null)
                return BadRequest("Invalid or empty patch document.");

            RestaurantSaveDTO restaurant = await _RestaurantService.FindSaveAsync(id);
            if (restaurant == null)
                return NotFound($"Restaurant with ID {id} was not found.");

            patchDoc.ApplyTo(restaurant, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(restaurant.Name))
                return BadRequest("Restaurant name cannot be empty.");

            if (restaurant.CreatedAt > DateTime.Now)
                return BadRequest("Creation date cannot be in the future.");

            if (!ValidateSubDomain(restaurant.SubDomain))
                return BadRequest("Invalid sub-domain format.");

            if (await _RestaurantService.UpdateAsync(id, restaurant))
                return Ok(restaurant);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the restaurant.");
        }


        [HttpPut("{id}", Name = "UpdateRestaurant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RestaurantSaveDTO>> UpdateRestaurant(int id, RestaurantSaveDTO updatedRestaurant)
        {
            if (id < 1)
                return BadRequest("Restaurant ID must be greater than 0.");

            if (updatedRestaurant == null)
                return BadRequest("Updated restaurant data cannot be empty.");

            if (string.IsNullOrWhiteSpace(updatedRestaurant.Name))
                return BadRequest("Restaurant name is required.");

            if (updatedRestaurant.CreatedAt > DateTime.Now)
                return BadRequest("Creation date cannot be in the future.");

            if (!ValidateSubDomain(updatedRestaurant.SubDomain))
                return BadRequest("Invalid sub-domain format.");

            if (!await _RestaurantService.IsExistAsync(id))
                return NotFound($"Restaurant with ID {id} was not found.");

            if (await _RestaurantService.UpdateAsync(id, updatedRestaurant))
                return Ok(updatedRestaurant);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the restaurant.");
        }


        [HttpDelete("{id}", Name = "DeleteRestaurant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteRestaurant(int id)
        {
            if (id < 1)
                return BadRequest("Restaurant ID must be greater than 0.");

            if (await _RestaurantService.DeleteAsync(id))
                return Ok("Restaurant was deleted successfully.");
            else
                return NotFound($"Restaurant with ID {id} was not found.");
        }
    }
}