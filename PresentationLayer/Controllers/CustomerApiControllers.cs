using BusinessLayer;
using BusinessLayer.DTOClasses.Customers;
using BusinessLayer.DTOClasses.Customers.CustomerDTO;
using BusinessLayer.DTOClasses.Customers.CustomerSaveDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        [HttpGet("All", Name = "GetAllCustomers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CustomerDTO>> GetAllCustomers()
        {
            List<CustomerDTO> CustomersList = CustomerService.GetAllCustomers();
            if (CustomersList.Count == 0)
            {
                return NotFound("no Customer found");
            }
            return Ok(CustomersList);
        }




        [HttpGet("{id}", Name = "FindCustomerByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CustomerDTO> FindCustomertByID(int id)
        {

            if (id < 1)
            {
                return BadRequest($"Not Accepted ID  : {id}");
            }
            CustomerService service= CustomerService.Find(id);
            if (service== null)
            {
                return NotFound($"Customer with ID {id} not found ");
            }
            return Ok(service.ConvertToDTO());

        }





        [HttpPost(Name = "PostAddCustomer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CustomerDTO> AddCustomer(CustomerSaveDTO newCustomer)
        {
            //DO NOT TAKE THE ID HERE
            if (newCustomer == null || string.IsNullOrEmpty(newCustomer.FirstName) || string.IsNullOrEmpty(newCustomer.LastName)
                || newCustomer.Phone.Length != 10 || string.IsNullOrEmpty(newCustomer.Phone)|| newCustomer.LastOrderDate > DateTime.Now)
            {
                //Better UI this way Change how u want tho
                if (newCustomer == null)
                {
                    return BadRequest("Customer Empty");
                }
                if (string.IsNullOrEmpty(newCustomer.FirstName))
                {
                    return BadRequest("Empty First name");
                }
                if (string.IsNullOrEmpty(newCustomer.LastName))
                {
                    return BadRequest("Empty last Name");
                }
                if (newCustomer.Phone.Length != 10)
                {
                    return BadRequest("NO 10's!!");
                }
                if (string.IsNullOrEmpty(newCustomer.Phone))
                {
                    return BadRequest("Empty Phone Number");
                }
                if (newCustomer.LastOrderDate > DateTime.Now)
                {
                    return BadRequest("OrderDate Not Rihgt");
                }
                return BadRequest("Not Accepted Data");
            }

            CustomerService customer = new CustomerService(newCustomer);
            int id = customer.Add();
            if (id != -1)
            {
               CustomerDTO customerDTO =  new CustomerDTO(id, newCustomer.FirstName,newCustomer.LastName,newCustomer.Phone,
                                                           newCustomer.NumberOfOrders,newCustomer.Email,newCustomer.LastOrderDate);
                return CreatedAtRoute("FindCustomerByID", new { Id = id },customerDTO);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the customer.");
        }





        [HttpPut("{id}",Name = "UpdateCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CustomerService> UpdateCustomer( int id,CustomerSaveDTO updatedCustomer)
        {
            if  (id < 1 || updatedCustomer == null || string.IsNullOrEmpty(updatedCustomer.FirstName) 
                || string.IsNullOrEmpty(updatedCustomer.LastName) || updatedCustomer.Phone.Length != 10 
                || string.IsNullOrEmpty(updatedCustomer.Phone) || updatedCustomer.LastOrderDate > DateTime.Now)
           
            {
                return BadRequest("Invalid customer data.");
            }

            CustomerService service = CustomerService.Find(id);
            if (service == null)
            {
                return NotFound($"Customer with ID {id} not found.");
            }
          

           if( service.Update(updatedCustomer))
               return Ok(service.ConvertToDTO());          
            else
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the customer.");

        }

        [HttpDelete("{id}", Name = "DeleteCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult DeleteCustomer(int id)
        {
            if (id < 1)
                return BadRequest("Invalid ID");
            if(CustomerService.Delete(id))
                return Ok("Customer was deleted");
            else
                return NotFound($"Customer with ID {id} not found.");

        }

    }
}
