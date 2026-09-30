using BusinessLayer.DTOClasses.Customers;
using BusinessLayer.DTOClasses.Customers.CustomerDTO;
using BusinessLayer.DTOClasses.Customers.CustomerSaveDTO;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static App.API.GloabalClasses.Validation;
namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly CustomerService _CustomerService;

        public CustomerController(CustomerService CustomerService)
        {
            _CustomerService = CustomerService;
        }

        [HttpGet("All", Name = "GetAllCustomers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAllCustomers()
        {

            List<CustomerDTO> CustomersList = await _CustomerService.GetAllCustomersAsync();
            if (CustomersList == null || CustomersList.Count == 0)
                return NotFound("No customers found");
            return Ok(CustomersList);
        }


        [HttpGet("{id}", Name = "FindCustomerByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerDTO>> FindCustomertByID(int id)
        {
            if (!int.TryParse(id.ToString(), out int parsedId))
            {
                return BadRequest($"ID must be an integer: {id}");
            }
            if (id < 1)
            {
                return BadRequest($"Not Accepted ID  : {id}");
            }
            CustomerDTO CustomerDTO = await  _CustomerService.FindAsync(id);
            if (CustomerDTO== null)
            {
                return NotFound($"Customer with ID {id} not found ");
            }
            return Ok(CustomerDTO);

        }





        [HttpPost(Name = "PostAddCustomer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CustomerDTO>> AddCustomer(CustomerSaveDTO newCustomer)
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
            if (!string.IsNullOrEmpty(newCustomer.Email) && !ValidateEmail(newCustomer.Email))
            {
                return BadRequest("Invalid Email");
            }
            if (newCustomer.LastOrderDate > DateTime.Now)
            {
                 return BadRequest("Invalid OrderDate");
            }
                
            int id = await  _CustomerService.AddAsync(newCustomer);
            if (id != -1)
            {
               CustomerDTO CustomerDTO =  new CustomerDTO(id, newCustomer.FirstName,newCustomer.LastName,newCustomer.Phone,
                                                          newCustomer.NumberOfOrders,newCustomer.Email,newCustomer.LastOrderDate,newCustomer.RestaurantId);
                return CreatedAtRoute("FindCustomerByID", new { Id = id },CustomerDTO);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the Customer.");
        }


        [HttpPatch("{id}",Name = "PatchCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerSaveDTO>> PatchCustomer(int id, [FromBody] JsonPatchDocument<CustomerSaveDTO> patchDoc)
        {
            if (patchDoc == null) return BadRequest("Invalid patch document.");

            CustomerSaveDTO customer = await _CustomerService.FindSaveAsync(id);
            if (customer == null) return NotFound($"Customer with ID {id} not found.");

            patchDoc.ApplyTo(customer, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(customer.FirstName))
                return BadRequest("FirstName cannot be empty.");
            if (string.IsNullOrWhiteSpace(customer.LastName))
                return BadRequest("LastName cannot be empty.");
            if (customer.Phone.Length != 10)
                return BadRequest("Invalid phon number");
            if (!string.IsNullOrWhiteSpace(customer.Email) && ! ValidateEmail(customer.Email))
                return BadRequest("Invalid Email");
            if ( customer.LastOrderDate>DateTime.Now)
                return BadRequest("Cannot be set to a future date.");
            if (customer.RestaurantId<1)
                return BadRequest($"Cannot assign customer to Restaurant ID {customer.RestaurantId}");

           if( await _CustomerService.UpdateAsync(id,customer))
            return Ok(customer);
           else
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the Customer.");

        }

        [HttpPut("{id}",Name = "UpdateCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CustomerService>> UpdateCustomer(int id,CustomerSaveDTO updatedCustomer)
        {
            if  (id < 1 || updatedCustomer == null || string.IsNullOrEmpty(updatedCustomer.FirstName) 
                || string.IsNullOrEmpty(updatedCustomer.LastName) || updatedCustomer.Phone.Length != 10 
                || string.IsNullOrEmpty(updatedCustomer.Phone) || updatedCustomer.LastOrderDate > DateTime.Now 
                ||(!string.IsNullOrEmpty(updatedCustomer.Email) && !ValidateEmail(updatedCustomer.Email)))
           
            {
                return BadRequest("Invalid Customer data.");
            }

            if (!await _CustomerService.IsExistAsync(id))
            {
                return NotFound($"Customer with ID {id} not found.");
            }
          

           if (await _CustomerService.UpdateAsync(id,updatedCustomer))
               return Ok(updatedCustomer);          
            else
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the Customer.");

        }

        [HttpDelete("{id}", Name = "DeleteCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult> DeleteCustomer(int id)
        {
            if (id < 1)
                return BadRequest("Invalid ID");
            if(await _CustomerService.DeleteAsync(id))
                return Ok("Customer was deleted");
            else
                return NotFound($"Customer with ID {id} not found.");

        }

    }
}
