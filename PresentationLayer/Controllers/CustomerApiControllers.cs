using EFDataAccessLayer.DTOClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerApiControllers : ControllerBase
    {

        [HttpGet("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CustomerDTO>> GetAllCustomers()
        {
            List<CustomerDTO> CustomersList = BusinessLayer.Customer.GetAllCustomers();
            if (CustomersList.Count == 0)
            {
                return NotFound("no students found");
            }
            return Ok(CustomersList);
        }
    }
}
