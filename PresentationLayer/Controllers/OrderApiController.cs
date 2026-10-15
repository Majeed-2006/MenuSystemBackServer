using BusinessLayer.DTOClasses.Customers.CustomerDTO;
using BusinessLayer.DTOClasses.Orders;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly OrderService _OrderService;

        public OrderController(OrderService OrderService)
        {
             _OrderService = OrderService;
        }

        [HttpGet("All", Name = "GetAllOrders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAllOrders()
        {
            List<OrderDTO> OrderList = await _OrderService.GetAllOrdersAsync();
            if (OrderList== null || OrderList.Count == 0)
                return NotFound("No customers found");
            return Ok(OrderList);
        }



        [HttpPost(Name = "PostAddOrder")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<OrderAddDTO>>> AddnewOrderOffline(OrderAddDTO newOrder)
        {
            if (newOrder == null)
            {
                return BadRequest("Order Empty");
            }
            else if (newOrder.CustomerID == null)
            {
                return BadRequest("Empty Customer ID ");
            }
          
            //this shouldnt be a check
            else if (newOrder.Type == EFDataAccessLayer.Enum.OrderType.Delivery)
            {
                if(newOrder.DriverID == null)
                  return BadRequest("Order is online");

                if (newOrder.WaiterID != null)
                    return BadRequest("Cannot put waiter");
            }
            else if(newOrder.Type == EFDataAccessLayer.Enum.OrderType.DineIn)
            {
                if (newOrder.WaiterID == null)
                    return BadRequest("Order is offline");

                if (newOrder.DriverID != null)
                    return BadRequest("Order is offline");
            }
            else if (newOrder.Type == EFDataAccessLayer.Enum.OrderType.Takeaway)
            {
                if (newOrder.WaiterID != null || newOrder.DriverID != null)
                    return BadRequest("Order is takeaway");
            }
            else if (newOrder.TotalPrice == null)
            {
                return BadRequest("Empty Total price");
            }
            else if (newOrder.OrderDateTime == null)
            {
                return BadRequest("order date time must be known");
            }
            else if (newOrder.Type == null)
            {
                return BadRequest("unknown order type");
            }
            else if (newOrder.OrderStatusID == null)
            {
                return BadRequest("Empty status ID ");
            }
            else if (newOrder.PaymentStatus == null)
            {
                return BadRequest("Empty payment status");
            }
            int Success = await _OrderService.AddAsync(newOrder);
            if (Success != null)
            {
                return Ok("Added new order");
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the Order");

        }
    }
}
