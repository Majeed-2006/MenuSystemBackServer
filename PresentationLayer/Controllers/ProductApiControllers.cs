using BusinessLayer.DTOClasses.Customers.CustomerDTO;
using BusinessLayer.DTOClasses.Orders;
using BusinessLayer.DTOClasses.Products;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ProductService _ProductrService;

        public ProductController(ProductService ProductService)
        {
            _ProductrService = ProductService;
        }

        [HttpGet("All", Name = "GetAllProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        //gonna make a second one based off the restraunt entered 
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {

            List<ProductDTO> productsList = await _ProductrService.GetAllProductsAsync();
            if (productsList == null || productsList.Count == 0)
                return NotFound("No customers found");
            return Ok(productsList);
        }



        [HttpGet("{id}", Name = "GetProductID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductID(int id)
        {
            if (id < 1)
            {
                return BadRequest("Product ID must be greater than 0.");
            }
            // its the same AddDTO as we dont need a more complex DTO for this 
            ProductDTO ProductDTO= await _ProductrService.FindAsync(id);

            if (ProductDTO == null)
                return NotFound($"Product with ID {id} was not found.");

            return Ok(ProductDTO);
        }



        //Imporatant note isAviable has a sepreate check from here
        [HttpPut("{id}", Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductDTO>> UpdateOrder(int id, ProductDTO updatedProduct)
        {
            if (id < 1)
                return BadRequest("Order ID must be greater than 0.");

            if (updatedProduct == null)
                return BadRequest("Updated Order data cannot be empty.");

            if (updatedProduct.CreatedByRestrauntID < 1)
                return BadRequest("ID is Invalid.");

            if (updatedProduct.CategoryID < 1)
                return BadRequest("Categoryid is Wrong.");


            if (updatedProduct.CreatedByUserID < 1)
                return BadRequest("User is Unknown.");

            if (string.IsNullOrWhiteSpace(updatedProduct.Name))
                return BadRequest("Name is Unknown.");
            

            if (string.IsNullOrWhiteSpace(updatedProduct.Description))
                return BadRequest("discription is incorrect.");


            if (updatedProduct.Price <=0)
                return BadRequest("Price is Not Correct.");
            
            if (await _ProductrService.UpdateAsync(id, updatedProduct))
                return Ok(updatedProduct);

            else
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the customer.");
        }


        /*
        [HttpPatch("{id}", Name = "PatchOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OrderDTO>> PatchOrder(int id, [FromBody] JsonPatchDocument<OrderDTO> patchDoc)
        {

        }
        */



        [HttpPost(Name = "PostAddProduct")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> AddnewOrderOffline(ProductDTO ProductDTO)
        {
            if (ProductDTO == null)
            {
                return BadRequest("Order Empty");
            }
            if (ProductDTO.CreatedByRestrauntID < 1)
                return BadRequest("ID is Invalid.");

            if (ProductDTO.CategoryID < 1)
                return BadRequest("Categoryid is Wrong.");


            if (ProductDTO.CreatedByUserID < 1)
                return BadRequest("User is Unknown.");

            if (string.IsNullOrWhiteSpace(ProductDTO.Name))
                return BadRequest("Name is Unknown.");


            if (string.IsNullOrWhiteSpace(ProductDTO.Description))
                return BadRequest("discription is incorrect.");


            if (ProductDTO.Price <= 0)
                return BadRequest("Price is Not Correct.");


            int Success = await _ProductrService.AddAsync(ProductDTO);
            if (Success != null)
            {
                return Ok("Added new Product");
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the Product");

        }



        [HttpDelete("{id}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            if (id < 1)
                return BadRequest("Product ID must be greater than 0.");

            if (!await _ProductrService.IsExistAsync(id))
                return NotFound($"Product with ID {id} was not found.");

            if (await _ProductrService.DeleteAsync(id))
                // prop not gonna work 
                return Ok("Product was deleted successfully.");
            else
                return Conflict("Cannot delete this Product because it is linked to other entities. Try a different TECH");
        }

    }
}
