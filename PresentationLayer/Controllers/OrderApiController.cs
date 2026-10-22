using BusinessLayer.DTOClasses.Customers.CustomerDTO;
using BusinessLayer.DTOClasses.Customers.CustomerSaveDTO;
using BusinessLayer.DTOClasses.Orders;
using BusinessLayer.Services;
using Microsoft.AspNetCore.JsonPatch;
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


        [HttpGet("{id}", Name = "GetOrderID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrderID(int id)
        {
            if (id <1)
            {
                return BadRequest("Order ID must be greater than 0.");
            }
            // its AddDTO as we dont need the more complex DTO for this 
            OrderAddDTO orderAddDTO = await _OrderService.FindAsync(id);

            if (orderAddDTO == null)
                return NotFound($"Order with ID {id} was not found.");

            return Ok(orderAddDTO);
        }



        [HttpGet("Restraunts/{id}", Name = "GetRestrauntOrders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetRestrauntOrders(int id)
        {
            if (id < 1)
            {
                return BadRequest("Restraunt ID must be greater than 0.");
            }
            // its AddDTO as we dont need the more complex DTO for this 
            List<OrderDTO> OrderList = await _OrderService.GetAllRestrauntOrdersAsync(id);

            if (OrderList == null)
                return NotFound($"Order with ID {id} was not found.");

            return Ok(OrderList);
        }




        [HttpPut("{id}", Name = "UpdateOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OrderSaveDTO>> UpdateOrder(int id, OrderSaveDTO updatedOrder)
        {
            if (id < 1)
                return BadRequest("Order ID must be greater than 0.");

            if (updatedOrder == null)
                return BadRequest("Updated Order data cannot be empty.");

            if (updatedOrder.TotalPrice <=0)
                return BadRequest("Total Price is Invalid.");

            if (updatedOrder.PaymentStatus == 0 )
                return BadRequest("payment status is Unknown.");

            if (updatedOrder.Type == 0)
            {
                return BadRequest("Type is Unknown.");
            }

            if (updatedOrder.OderStatusID <1)
                return BadRequest("orderstatus is incorrect.");

            if (await _OrderService.UpdateAsync(id, updatedOrder))
                return Ok(updatedOrder);

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



        [HttpDelete("{id}", Name = "DeleteOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            if (id < 1)
                return BadRequest("Order ID must be greater than 0.");

            if (!await _OrderService.IsExistAsync(id))
                return NotFound($"Order with ID {id} was not found.");

            if (await _OrderService.DeleteAsync(id))
                // prop not gonna work 
                return Ok("Order was deleted successfully.");
            else
                return Conflict("Cannot delete this Order because it is linked to other entities. Try a different TECH");
        }


    }
}
