
using BusinessLayer.DTOClasses;
using EFDataAccessLayer.DatbaseClasses;
using EFDataAccessLayer.EntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class RoleService
    {
        private readonly RoleDataAccess _RoleDataAccess;

        public RoleService(RoleDataAccess roleDataAccess)
        {
            _RoleDataAccess = roleDataAccess;
        }

        public async Task<List<RoleDTO>> GetAllRolesAsync()
        {
            var roles = await _RoleDataAccess.GetAllRolesAsync();
            if (roles == null || !roles.Any())
            {
              //  throw new BusinessException("empty List");
            }

            return roles.Select(r => new RoleDTO(
                r.Id,
                r.Name
            )).ToList();
        }

        public async Task<RoleDTO?> FindAsync(int id)
        {
            Role role = await _RoleDataAccess.FindAsync(id);
            if (role != null)
            {
                var roleDTO = new RoleDTO(
                    role.Id,
                    role.Name
                );
                return roleDTO;
            }
            return null;
        }
    }
}