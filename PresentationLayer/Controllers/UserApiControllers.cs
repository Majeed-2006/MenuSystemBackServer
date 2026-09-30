using BusinessLayer;
using BusinessLayer.DTOClasses.Users;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using static App.API.GloabalClasses.Validation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController:ControllerBase
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
                return NotFound("No users found");

            return Ok(UsersList);
        }


        [HttpGet("{id}", Name = "FindUserByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDTO>> FindUsertByID(int id)
        {
            if (!int.TryParse(id.ToString(), out int parsedId))
            {
                return BadRequest($"ID must be an integer: {id}");
            }
            if (id < 1)
            {
                return BadRequest("ID must be greater than 0.");
            }
            UserDTO UserDTO = await _UserService.FindAsync(id);
            if (UserDTO == null)
            {
                return NotFound($"User with ID {id} not found ");
            }
            return Ok(UserDTO);

        }





        [HttpPost(Name = "PostAddUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDTO>> AddUser(UserAddDTO newUser)
        {
            //Better UI this way Change how u want tho
            if (newUser == null)
            {
                return BadRequest("User Empty");
            }
            if (string.IsNullOrEmpty(newUser.FirstName))
            {
                return BadRequest("Empty First name");
            }
            if (string.IsNullOrEmpty(newUser.LastName))
            {
                return BadRequest("Empty last Name");
            }
            if (newUser.Phone.Length != 10)
            {
                return BadRequest("NO 10's!!");
            }
            if (string.IsNullOrEmpty(newUser.Password))
            {
                return BadRequest("Empty Password");
            }
            if (string.IsNullOrEmpty(newUser.UserName))
            {
                return BadRequest("Empty UserName");
            }
            if (string.IsNullOrEmpty(newUser.Email))
            {
                return BadRequest("Empty Email");
            }
            if (!ValidateEmail(newUser.Email))
            {
                return BadRequest("Invalid Email");
            }
            if (!ValidatePassword(newUser.Password))
            {
                return BadRequest("Invalid Password");
            }
            int id = await _UserService.AddAsync(newUser);
            if (id != -1)
            {
                return CreatedAtRoute("FindUserByID", new { Id = id }, newUser);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the User.");
        }



        [HttpPatch("{id}", Name = "PatchUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserSaveDTO>> PatchCustomer(int id, [FromBody] JsonPatchDocument<UserSaveDTO> patchDoc)
        {
            if (patchDoc == null) return BadRequest("Invalid patch document.");

            UserSaveDTO user = await _UserService.FindSaveAsync(id);
            if (user == null) return NotFound($"User with ID {id} not found.");

            patchDoc.ApplyTo(user, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(user.FirstName))
                return BadRequest("FirstName cannot be empty.");
            if (string.IsNullOrWhiteSpace(user.LastName))
                return BadRequest("LastName cannot be empty.");
            if (string.IsNullOrWhiteSpace(user.UserName))
                return BadRequest("UserName cannot be empty.");
            if (user.Phone.Length != 10)
                return BadRequest("Invalid phon number");
            if (!ValidateEmail(user.Email))
                return BadRequest("Invalid Email");
            if (string.IsNullOrWhiteSpace(user.Address))
                return BadRequest("Address cannot be empty.");
            if (user.RestaurantId < 1 )
                return BadRequest($"Invalid restaurant ID");
            if (user.RoleId < 1)
                return BadRequest($"Invalid role ID");
            if (user.ManagerId< 1)
                return BadRequest($"Invalid manager ID");

            if (await _UserService.UpdateAsync(id, user))
                return Ok(user);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the Customer.");

        }


        [HttpPut("{id}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserSaveDTO>> UpdateUser(int id, UserSaveDTO updatedUser)
        {
            if (!int.TryParse(id.ToString(), out int parsedId))
            {
                return BadRequest($"ID must be an integer: {id}");
            }
         
            if (id < 1 ||updatedUser.RestaurantId <1 || (updatedUser.RoleId<1 && updatedUser.RoleId!=null))
            {
                return BadRequest("IDs must be greater than 0.");
            }
            //Better UI this way Change how u want tho
            if (updatedUser == null)
            {
                return BadRequest("User Empty");
            }
            if (string.IsNullOrEmpty(updatedUser.FirstName))
            {
                return BadRequest("Empty First name");
            }
            if (string.IsNullOrEmpty(updatedUser.LastName))
            {
                return BadRequest("Empty last Name");
            }
            if (updatedUser.Phone.Length != 10)
            {
                return BadRequest("NO 10's!!");
            }
            if (string.IsNullOrEmpty(updatedUser.UserName))
            {
                return BadRequest("Empty UserName");
            }
            if (string.IsNullOrEmpty(updatedUser.Email))
            {
                return BadRequest("Empty Email");
            }
            if (!ValidateEmail(updatedUser.Email))
            {
                return BadRequest("Invalid Email");
            }
            if (await _UserService.UpdateAsync(id, updatedUser))
            {
                return Ok(updatedUser);
            }
            else
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the User.");

        }

        [HttpPut(Name = "ChangeUserPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ChangePassword(UserChangePasswordDTO userChangePasswordDTO)
        {
            if (userChangePasswordDTO == null)
            {
                return BadRequest("User Empty");
            }
           
            if(userChangePasswordDTO.CurrentPassword == userChangePasswordDTO.NewPassword)
            {
                return BadRequest("new passwod and curent password are same");
            }
            if (string.IsNullOrEmpty(userChangePasswordDTO.NewPassword))
            {
                return BadRequest("Empty New Password");
            }
            if (string.IsNullOrEmpty(userChangePasswordDTO.UserName))
            {
                return BadRequest("Empty user name");
            }
            if (string.IsNullOrEmpty(userChangePasswordDTO.CurrentPassword))
            {
                return BadRequest("Empty Current Passsword");
            }
            if (!ValidatePassword(userChangePasswordDTO.NewPassword))
            {
                return BadRequest("Invalid New Password");
            }
            if (!ValidatePassword(userChangePasswordDTO.CurrentPassword))
            {
                return BadRequest("Invalid Current  Password");
            }

            if (await _UserService.ChangePasswordAsync(userChangePasswordDTO))
                return Ok("Passwaord was changed Successfully");
            else
                return NotFound($"User With user name{userChangePasswordDTO.UserName} Not Found");
        }

        [HttpDelete("{id}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult> DeleteUser(int id)
        {
            if (id < 1)
                return BadRequest("Invalid ID");

            if (!int.TryParse(id.ToString(), out int parsedId))
                return BadRequest($"ID must be an integer: {id}");
            
            if (await _UserService.DeleteAsync(id))
                return Ok("User was deleted");
            else
                return NotFound($"User with ID {id} not found.");

        }


        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Login([FromBody] UserLoginDTO loginDTO)
        {
            if (loginDTO == null)
            {
                return BadRequest("User Empty");
            }
            if (string.IsNullOrEmpty(loginDTO.Password))
            {
                return BadRequest("Empty Password");
            }
            if (string.IsNullOrEmpty(loginDTO.UserName))
            {
                return BadRequest("Empty UserName");
            }
            if (!ValidatePassword(loginDTO.Password))
            {
                return BadRequest("Invalid Password");
            }
            if (await _UserService.LoginAsync(loginDTO))
                return Ok("Valid User");
            else
                return NotFound("Invalid User");
        }
    }
}
