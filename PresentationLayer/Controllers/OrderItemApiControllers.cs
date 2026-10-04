using BusinessLayer.Services;
using EFDataAccessLayer.DTOClasses;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {

        private readonly OrderItemService _OrderItemsService;

        public OrderItemController(OrderItemService OrderItemService)
        {
            _OrderItemsService = OrderItemService;
        }

        [HttpGet("All", Name = "GetAllOrderItems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OrderItemDTO>>> GetAllCustomers()
        {

            List<OrderItemDTO> OrderItemList = await _OrderItemsService.GetAllOrderItemsAsync();
            if (OrderItemList == null || OrderItemList.Count == 0)
                return NotFound("No customers found");
            return Ok(OrderItemList);
        }
    }
}
