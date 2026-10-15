using EFDataAccessLayer.SettingClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDataAccessLayer.EntityClasses;
using Microsoft.EntityFrameworkCore;

namespace EFDataAccessLayer.DatbaseClasses
{
    public class RoleDataAccess
    {
        private readonly AppDbContext _Context;

        public RoleDataAccess(AppDbContext Context)
        {
            _Context = Context;
        }

        public async Task<List<Role>> GetAllRolesAsync()
        {
            try
            {
                return await _Context.Roles.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Role> FindAsync(int id, bool isTracking = false)
        {
            try
            {
                if (!isTracking)
                    return await _Context.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
                else
                    return await _Context.Roles.FirstOrDefaultAsync(r => r.Id == id);
            }
            catch
            {
                return null;
            }
        }
    }
}