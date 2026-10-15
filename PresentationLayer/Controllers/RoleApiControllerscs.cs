using BusinessLayer.DTOClasses;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleService _RoleService;

        public RolesController(RoleService roleService)
        {
            _RoleService = roleService;
        }

        [HttpGet("All", Name = "GetAllRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> GetAllRoles()
        {
            List<RoleDTO> rolesList = await _RoleService.GetAllRolesAsync();
            if (rolesList == null || rolesList.Count == 0)
                return NotFound("No roles found");

            return Ok(rolesList);
        }

        [HttpGet("{id}", Name = "FindRoleByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoleDTO>> FindRoleByID(int id)
        {
            if (id < 1)
                return BadRequest($"Not Accepted ID : {id}");

            RoleDTO roleDTO = await _RoleService.FindAsync(id);
            if (roleDTO == null)
                return NotFound($"Role with ID {id} not found ");

            return Ok(roleDTO);
        }
    }
}