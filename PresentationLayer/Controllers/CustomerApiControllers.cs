using BusinessLayer.DTOClasses.Customers.CustomerDTO;
using BusinessLayer.DTOClasses.Customers.CustomerSaveDTO;
using BusinessLayer.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
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
                return NotFound("No customers found.");

            return Ok(CustomersList);
        }


        [HttpGet("{id}", Name = "FindCustomerByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerDTO>> FindCustomertByID(int id)
        {
            if (id < 1)
                return BadRequest("Customer ID must be greater than 0.");

            CustomerDTO CustomerDTO = await _CustomerService.FindAsync(id);

            if (CustomerDTO == null)
                return NotFound($"Customer with ID {id} was not found.");

            return Ok(CustomerDTO);
        }


        [HttpPost(Name = "PostAddCustomer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CustomerDTO>> AddCustomer(CustomerSaveDTO newCustomer)
        {
            if (newCustomer == null)
                return BadRequest("Customer data cannot be empty.");

            if (string.IsNullOrWhiteSpace(newCustomer.FirstName))
                return BadRequest("First name is required.");

            if (string.IsNullOrWhiteSpace(newCustomer.LastName))
                return BadRequest("Last name is required.");

            if (string.IsNullOrWhiteSpace(newCustomer.Phone))
                return BadRequest("Phone number is required.");

            if (newCustomer.Phone.Length != 10)
                return BadRequest("Phone number must be exactly 10 digits.");

            if (!string.IsNullOrEmpty(newCustomer.Email) && !ValidateEmail(newCustomer.Email))
                return BadRequest("Invalid email address format.");

            if (newCustomer.LastOrderDate > DateTime.Now)
                return BadRequest("Last order date cannot be in the future.");

            int id = await _CustomerService.AddAsync(newCustomer);

            if (id != -1)
            {
                CustomerDTO CustomerDTO = new CustomerDTO(
                    id,
                    newCustomer.FirstName,
                    newCustomer.LastName,
                    newCustomer.Phone,
                    newCustomer.NumberOfOrders,
                    newCustomer.Email,
                    newCustomer.LastOrderDate,
                    newCustomer.RestaurantId
                );

                return CreatedAtRoute("FindCustomerByID", new { Id = id }, CustomerDTO);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the customer.");
        }

        [HttpPatch("{id}", Name = "PatchCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CustomerSaveDTO>> PatchCustomer(int id, [FromBody] JsonPatchDocument<CustomerSaveDTO> patchDoc)
        {
            if (id < 1)
                return BadRequest("Customer ID must be greater than 0.");

            if (patchDoc == null)
                return BadRequest("Invalid or empty patch document.");

            CustomerSaveDTO customer = await _CustomerService.FindSaveAsync(id);

            if (customer == null)
                return NotFound($"Customer with ID {id} was not found.");

            patchDoc.ApplyTo(customer, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(customer.FirstName))
                return BadRequest("First name cannot be empty.");

            if (string.IsNullOrWhiteSpace(customer.LastName))
                return BadRequest("Last name cannot be empty.");

            if (string.IsNullOrWhiteSpace(customer.Phone) || customer.Phone.Length != 10)
                return BadRequest("Phone number must be exactly 10 digits.");

            if (!string.IsNullOrWhiteSpace(customer.Email) && !ValidateEmail(customer.Email))
                return BadRequest("Invalid email address format.");

            if (customer.LastOrderDate > DateTime.Now)
                return BadRequest("Last order date cannot be in the future.");

            if (customer.RestaurantId < 1)
                return BadRequest($"Invalid Restaurant ID: {customer.RestaurantId}.");

            if (await _CustomerService.UpdateAsync(id, customer))
                return Ok(customer);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the customer.");
        }


        [HttpPut("{id}", Name = "UpdateCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CustomerSaveDTO>> UpdateCustomer(int id, CustomerSaveDTO updatedCustomer)
        {
            if (id < 1)
                return BadRequest("Customer ID must be greater than 0.");

            if (updatedCustomer == null)
                return BadRequest("Updated customer data cannot be empty.");

            if (string.IsNullOrWhiteSpace(updatedCustomer.FirstName))
                return BadRequest("First name is required.");

            if (string.IsNullOrWhiteSpace(updatedCustomer.LastName))
                return BadRequest("Last name is required.");

            if (string.IsNullOrWhiteSpace(updatedCustomer.Phone) || updatedCustomer.Phone.Length != 10)
                return BadRequest("Phone number must be exactly 10 digits.");

            if (!string.IsNullOrEmpty(updatedCustomer.Email) && !ValidateEmail(updatedCustomer.Email))
                return BadRequest("Invalid email address format.");

            if (updatedCustomer.LastOrderDate > DateTime.Now)
                return BadRequest("Last order date cannot be in the future.");

            if (updatedCustomer.RestaurantId < 1)
                return BadRequest($"Invalid Restaurant ID: {updatedCustomer.RestaurantId}.");

            if (!await _CustomerService.IsExistAsync(id))
                return NotFound($"Customer with ID {id} was not found.");

            if (await _CustomerService.UpdateAsync(id, updatedCustomer))
                return Ok(updatedCustomer);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the customer.");
        }


        [HttpDelete("{id}", Name = "DeleteCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            if (id < 1)
                return BadRequest("Customer ID must be greater than 0.");

            if (await _CustomerService.DeleteAsync(id))
                return Ok("Customer was deleted successfully.");
            else
                return NotFound($"Customer with ID {id} was not found.");
        }
    }
}