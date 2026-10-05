using BusinessLayer.DTOClasses;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;
using static BusinessLayer.DTOClasses.OrderStatusDTO;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {
        private readonly OrderStatusService _OrderStatusService;

        public OrderStatusController(OrderStatusService orderStatusService)
        {
            _OrderStatusService = orderStatusService;
        }

        [HttpGet("All", Name = "GetAllOrderStatuses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OrderStatusDTO>>> GetAllOrderStatuses()
        {
            List<OrderStatusDTO> orderStatusesList = await _OrderStatusService.GetAllOrderStatusesAsync();
            if (orderStatusesList == null || orderStatusesList.Count == 0)
                return NotFound("No order status found");

            return Ok(orderStatusesList);
        }

        [HttpGet("{id}", Name = "FindOrderStatusByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderStatusDTO>> FindOrderStatusByID(int id)
        {
            if (id < 1)
                return BadRequest($"Not Accepted ID : {id}");

            OrderStatusDTO orderStatusDTO = await _OrderStatusService.FindAsync(id);
            if (orderStatusDTO == null)
                return NotFound($"Order Status with ID {id} not found ");

            return Ok(orderStatusDTO);
        }
    }
}