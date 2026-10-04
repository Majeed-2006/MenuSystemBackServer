using BusinessLayer.DTOClasses.Customers.CustomerDTO;
using BusinessLayer.Services;
using EFDataAccessLayer.DTOClasses;
using EFDataAccessLayer.EntityClasses;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {

        private readonly OrderStatusService _OrderStatusService;

        public OrderStatusController(OrderStatusService OrderStatusService)
        {
            _OrderStatusService = OrderStatusService;
        }

        [HttpGet("All", Name = "GetAllOrderStatues")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OrderStatusDTO>>> GetAllCustomers()
        {

            List<OrderStatusDTO> OrderStatusList = await _OrderStatusService.GetAllOrderStatusesAsync();
            if (OrderStatusList == null || OrderStatusList.Count == 0)
                return NotFound("No customers found");
            return Ok(OrderStatusList);
        }
    }
}
