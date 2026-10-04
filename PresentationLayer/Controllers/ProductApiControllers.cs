using BusinessLayer.DTOClasses.Customers.CustomerDTO;
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



    }
}
