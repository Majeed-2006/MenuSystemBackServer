using BusinessLayer.DTOClasses.OrderItems;
using BusinessLayer.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly OrderItemService _OrderItemService;

        public OrderItemController(OrderItemService OrderItemService)
        {
            _OrderItemService = OrderItemService;
        }

        [HttpGet("All", Name = "GetAllOrderItems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OrderItemDTO>>> GetAllOrderItems()
        {
            List<OrderItemDTO> OrderItemsList = await _OrderItemService.GetAllOrderItemsAsync();

            if (OrderItemsList == null || OrderItemsList.Count == 0)
                return NotFound("No order items found.");

            return Ok(OrderItemsList);
        }


        [HttpGet("{id}", Name = "FindOrderItemByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderItemDTO>> FindOrderItemByID(int id)
        {
            if (id < 1)
                return BadRequest("Order Item ID must be greater than 0.");

            OrderItemDTO OrderItemDTO = await _OrderItemService.FindAsync(id);

            if (OrderItemDTO == null)
                return NotFound($"Order item with ID {id} was not found.");

            return Ok(OrderItemDTO);
        }


        [HttpPost(Name = "PostAddOrderItem")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OrderItemDTO>> AddOrderItem(OrderItemSaveDTO newOrderItem)
        {
            if (newOrderItem == null)
                return BadRequest("Order item data cannot be empty.");

            if (newOrderItem.Quantity <= 0)
                return BadRequest("Quantity must be greater than zero.");

            if (newOrderItem.UnitPrice <= 0)
                return BadRequest("Unit price must be greater than zero.");

            if (newOrderItem.OrderID < 1)
                return BadRequest("Order ID must be greater than 0.");

            if (newOrderItem.ProductID < 1)
                return BadRequest("Product ID must be greater than 0.");

            int id = await _OrderItemService.AddAsync(newOrderItem);

            if (id != -1)
            {
                OrderItemDTO OrderItemDTO = new OrderItemDTO(
                    id,
                    newOrderItem.OrderID,
                    newOrderItem.Quantity,
                    newOrderItem.ProductID,
                    newOrderItem.UnitPrice
                );

                return CreatedAtRoute("FindOrderItemByID", new { Id = id }, OrderItemDTO);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the order item.");
        }


        [HttpPatch("{id}", Name = "PatchOrderItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OrderItemSaveDTO>> PatchOrderItem(int id, [FromBody] JsonPatchDocument<OrderItemSaveDTO> patchDoc)
        {
            if (id < 1)
                return BadRequest("Order Item ID must be greater than 0.");

            if (patchDoc == null)
                return BadRequest("Invalid or empty patch document.");

            OrderItemSaveDTO orderItem = await _OrderItemService.FindSaveAsync(id);

            if (orderItem == null)
                return NotFound($"Order item with ID {id} was not found.");

            patchDoc.ApplyTo(orderItem, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (orderItem.Quantity <= 0)
                return BadRequest("Quantity must be greater than zero.");

            if (orderItem.UnitPrice <= 0)
                return BadRequest("Unit price must be greater than zero.");

            if (orderItem.OrderID < 1)
                return BadRequest($"Invalid Order ID: {orderItem.OrderID}.");

            if (orderItem.ProductID < 1)
                return BadRequest($"Invalid Product ID: {orderItem.ProductID}.");

            if (await _OrderItemService.UpdateAsync(id, orderItem))
                return Ok(orderItem);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the order item.");
        }


        [HttpPut("{id}", Name = "UpdateOrderItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OrderItemSaveDTO>> UpdateOrderItem(int id, OrderItemSaveDTO updatedOrderItem)
        {
            if (id < 1)
                return BadRequest("Order Item ID must be greater than 0.");

            if (updatedOrderItem == null)
                return BadRequest("Updated order item data cannot be empty.");

            if (updatedOrderItem.Quantity <= 0)
                return BadRequest("Quantity must be greater than zero.");

            if (updatedOrderItem.UnitPrice <= 0)
                return BadRequest("Unit price must be greater than zero.");

            if (updatedOrderItem.OrderID < 1)
                return BadRequest("Order ID must be greater than 0.");

            if (updatedOrderItem.ProductID < 1)
                return BadRequest("Product ID must be greater than 0.");

            if (!await _OrderItemService.IsExistAsync(id))
                return NotFound($"Order item with ID {id} was not found.");

            if (await _OrderItemService.UpdateAsync(id, updatedOrderItem))
                return Ok(updatedOrderItem);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the order item.");
        }


        [HttpDelete("{id}", Name = "DeleteOrderItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteOrderItem(int id)
        {
            if (id < 1)
                return BadRequest("Order Item ID must be greater than 0.");

            if (await _OrderItemService.DeleteAsync(id))
                return Ok("Order item was deleted successfully.");
            else
                return NotFound($"Order item with ID {id} was not found.");
        }
    }
}