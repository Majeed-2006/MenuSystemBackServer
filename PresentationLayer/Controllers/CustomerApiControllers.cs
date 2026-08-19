using BusinessLayer;
using EFDataAccessLayer.DTOClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerApiControllers : ControllerBase
    {

        [HttpGet("All", Name = "GetAllCustomers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CustomerDTO>> GetAllCustomers()
        {
            List<CustomerDTO> CustomersList = BusinessLayer.Customer.GetAllCustomers();
            if (CustomersList.Count == 0)
            {
                return NotFound("no Customer found");
            }
            return Ok(CustomersList);
        }

        [HttpGet("{Id}", Name = "FindCustomerByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CustomerDTO> GetStudentByID(int Id)
        {

            if (Id < 1)
            {
                return BadRequest($"Not Accepted ID  : {Id}");
            }
            BusinessLayer.Customer customer = BusinessLayer.Customer.Find(Id);
            if (customer == null)
            {
                return NotFound($"Customer with ID {Id} not found ");
            }
            return Ok(customer.CDTO);

        }

        [HttpPost(Name = "PostAddCustomer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CustomerDTO> AddCustomer(CustomerDTO newCustomer)
        {
            if (newCustomer == null || string.IsNullOrEmpty(newCustomer.FirstName) || string.IsNullOrEmpty(newCustomer.LastName)
                || newCustomer.Phone.Length != 10 || string.IsNullOrEmpty(newCustomer.Phone)|| newCustomer.LastOrderDate > DateTime.Now)
            {
                return BadRequest("Not Accepted Data");
            }
            BusinessLayer.Customer customer = new BusinessLayer.Customer(newCustomer);
           
            if (customer.Add())
            {
                newCustomer.Id = customer.Id;
                return CreatedAtRoute("FindCustomerByID", new { Id = newCustomer.Id }, newCustomer);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the customer.");
        }
        [HttpPut(Name = "UpdateCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Customer> UpdateCustomer( CustomerDTO updatedCustomer)
        {
            if (updatedCustomer.Id < 1 || updatedCustomer == null || string.IsNullOrEmpty(updatedCustomer.FirstName) 
                || string.IsNullOrEmpty(updatedCustomer.LastName) || updatedCustomer.Phone.Length != 10 
                || string.IsNullOrEmpty(updatedCustomer.Phone) || updatedCustomer.LastOrderDate > DateTime.Now)
           
            {
                return BadRequest("Invalid customer data.");
            }

            BusinessLayer.Customer customer = BusinessLayer.Customer.Find(updatedCustomer.Id);

            if (customer == null)
            {
                return NotFound($"Customer with ID {updatedCustomer.Id} not found.");
            }

            customer.FirstName = updatedCustomer.FirstName;
            customer.LastName = updatedCustomer.LastName;
            customer.Email = updatedCustomer.Email;
            customer.Phone = updatedCustomer.Phone;
            customer.NumberOfOrders = updatedCustomer.NumberOfOrders;
            customer.LastOrderDate = updatedCustomer.LastOrderDate;
            customer.Update();
            return Ok(customer);
        }



        [HttpDelete("{Id}", Name = "DeleteCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult DeleteStudent(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not Accepted ID  : {id}");
            }

            if (BusinessLayer.Customer.Delete(id))
            {
                return Ok($"Customer with ID : {id} has been deleted");
            }
            else
            {
                return NotFound($"Customer with ID {id} not found ");
            }


        }

    }
}
