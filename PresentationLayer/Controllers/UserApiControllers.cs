using BusinessLayer;
using BusinessLayer.DTOClasses.Users;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using static App.API.GloabalClasses.Validation;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _UserService;

        public UserController(UserService UserService)
        {
            _UserService = UserService;
        }


        [HttpGet("All", Name = "GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            List<UserDTO> UsersList = await _UserService.GetAllUsersAsync();

            if (UsersList == null || UsersList.Count == 0)
                return NotFound("No users found.");

            return Ok(UsersList);
        }


        [HttpGet("{id}", Name = "FindUserByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDTO>> FindUsertByID(int id)
        {
            if (id < 1)
                return BadRequest("User ID must be greater than 0.");

            UserDTO UserDTO = await _UserService.FindAsync(id);

            if (UserDTO == null)
                return NotFound($"User with ID {id} was not found.");

            return Ok(UserDTO);
        }


        [HttpPost(Name = "PostAddUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDTO>> AddUser(UserAddDTO newUser)
        {
            if (newUser == null)
                return BadRequest("User data cannot be empty.");

            if (string.IsNullOrWhiteSpace(newUser.FirstName))
                return BadRequest("First name is required.");

            if (string.IsNullOrWhiteSpace(newUser.LastName))
                return BadRequest("Last name is required.");

            if (string.IsNullOrWhiteSpace(newUser.UserName))
                return BadRequest("Username is required.");

            if (string.IsNullOrWhiteSpace(newUser.Phone) || newUser.Phone.Length != 10)
                return BadRequest("Phone number must be exactly 10 digits.");

            if (string.IsNullOrWhiteSpace(newUser.Email))
                return BadRequest("Email address is required.");

            if (!ValidateEmail(newUser.Email))
                return BadRequest("Invalid email format.");

            if (string.IsNullOrWhiteSpace(newUser.Password))
                return BadRequest("Password is required.");

            if (!ValidatePassword(newUser.Password))
                return BadRequest("Password does not meet the security criteria.");

            int id = await _UserService.AddAsync(newUser);

            if (id != -1)
            {
                UserDTO user = new(id, newUser.FirstName, newUser.LastName, newUser.UserName, newUser.Email,
                                   newUser.Phone, newUser.Address, newUser.RoleId, newUser.RestaurantId, newUser.ManagerId);
                return CreatedAtRoute("FindUserByID", new { Id = id }, user);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the user.");
        }


        [HttpPatch("{id}", Name = "PatchUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserSaveDTO>> PatchUser(int id, [FromBody] JsonPatchDocument<UserSaveDTO> patchDoc)
        {
            if (id < 1)
                return BadRequest("User ID must be greater than 0.");

            if (patchDoc == null)
                return BadRequest("Invalid or empty patch document.");

            UserSaveDTO user = await _UserService.FindSaveAsync(id);

            if (user == null)
                return NotFound($"User with ID {id} was not found.");

            patchDoc.ApplyTo(user, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(user.FirstName))
                return BadRequest("First name cannot be empty.");

            if (string.IsNullOrWhiteSpace(user.LastName))
                return BadRequest("Last name cannot be empty.");

            if (string.IsNullOrWhiteSpace(user.UserName))
                return BadRequest("Username cannot be empty.");

            if (string.IsNullOrWhiteSpace(user.Phone) || user.Phone.Length != 10)
                return BadRequest("Phone number must be exactly 10 digits.");

            if (!ValidateEmail(user.Email))
                return BadRequest("Invalid email format.");

            if (string.IsNullOrWhiteSpace(user.Address))
                return BadRequest("Address cannot be empty.");

            if (user.RestaurantId < 1)
                return BadRequest("Restaurant ID must be greater than 0.");

            if (user.RoleId < 1)
                return BadRequest("Role ID must be greater than 0.");

            if (user.ManagerId < 1)
                return BadRequest("Manager ID must be greater than 0.");

            if (await _UserService.UpdateAsync(id, user))
                return Ok(user);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the user.");
        }


        [HttpPut("{id}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserSaveDTO>> UpdateUser(int id, UserSaveDTO updatedUser)
        {
            if (id < 1)
                return BadRequest("User ID must be greater than 0.");

            if (updatedUser == null)
                return BadRequest("Updated user data cannot be empty.");

            if (updatedUser.RestaurantId < 1 || (updatedUser.RoleId != null && updatedUser.RoleId < 1))
                return BadRequest("Restaurant ID and Role ID must be greater than 0.");

            if (string.IsNullOrWhiteSpace(updatedUser.FirstName))
                return BadRequest("First name is required.");

            if (string.IsNullOrWhiteSpace(updatedUser.LastName))
                return BadRequest("Last name is required.");

            if (string.IsNullOrWhiteSpace(updatedUser.UserName))
                return BadRequest("Username is required.");

            if (string.IsNullOrWhiteSpace(updatedUser.Phone) || updatedUser.Phone.Length != 10)
                return BadRequest("Phone number must be exactly 10 digits.");

            if (string.IsNullOrWhiteSpace(updatedUser.Email))
                return BadRequest("Email address is required.");

            if (!ValidateEmail(updatedUser.Email))
                return BadRequest("Invalid email format.");

            if (await _UserService.UpdateAsync(id, updatedUser))
                return Ok(updatedUser);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the user.");
        }


        [HttpPut("ChangePassword", Name = "ChangeUserPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ChangePassword([FromBody] UserChangePasswordDTO userChangePasswordDTO)
        {
            if (userChangePasswordDTO == null)
                return BadRequest("Password change data cannot be empty.");

            if (string.IsNullOrWhiteSpace(userChangePasswordDTO.UserName))
                return BadRequest("Username is required.");

            if (string.IsNullOrWhiteSpace(userChangePasswordDTO.CurrentPassword))
                return BadRequest("Current password is required.");

            if (string.IsNullOrWhiteSpace(userChangePasswordDTO.NewPassword))
                return BadRequest("New password is required.");

            if (userChangePasswordDTO.CurrentPassword == userChangePasswordDTO.NewPassword)
                return BadRequest("New password cannot be the same as the current password.");

            if (!ValidatePassword(userChangePasswordDTO.CurrentPassword))
                return BadRequest("Invalid current password format.");

            if (!ValidatePassword(userChangePasswordDTO.NewPassword))
                return BadRequest("Invalid new password format.");

            if (await _UserService.ChangePasswordAsync(userChangePasswordDTO))
                return Ok("Password was changed successfully.");
            else
                return NotFound($"User with username '{userChangePasswordDTO.UserName}' was not found.");
        }


        [HttpDelete("{id}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteUser(int id)
        {
            if (id < 1)
                return BadRequest("User ID must be greater than 0.");

            if (await _UserService.DeleteAsync(id))
                return Ok("User was deleted successfully.");
            else
                return NotFound($"User with ID {id} was not found.");
        }


        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Login([FromBody] UserLoginDTO loginDTO)
        {
            if (loginDTO == null)
                return BadRequest("Login credentials cannot be empty.");

            if (string.IsNullOrWhiteSpace(loginDTO.UserName))
                return BadRequest("Username is required.");

            if (string.IsNullOrWhiteSpace(loginDTO.Password))
                return BadRequest("Password is required.");

            if (!ValidatePassword(loginDTO.Password))
                return BadRequest("Invalid password format.");

            if (await _UserService.LoginAsync(loginDTO))
                return Ok("Login successful.");
            else
                return Unauthorized("Invalid username or password.");
        }
    }
}