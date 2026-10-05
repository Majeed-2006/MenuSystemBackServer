using EFDataAccessLayer.EntityClasses;
using EFDataAccessLayer.SettingClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLayer.DatbaseClasses
{
    public  class UserDataAccess
    {
        private readonly AppDbContext _Context;

        public UserDataAccess(AppDbContext context)
        {
            _Context = context;
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                return await _Context.Users.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<User?> FindAsync(int id, bool isTracking = false)
        {
            if(!isTracking)
             return await _Context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            else
                return await _Context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateAsync(User user)
        {
            User UpdatedUser = await _Context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);

            if (UpdatedUser != null)
            {
                UpdatedUser.FirstName = user.FirstName;
                UpdatedUser.LastName = user.LastName;
                UpdatedUser.Email = user.Email;
                UpdatedUser.Phone = user.Phone;
                UpdatedUser.UserName = user.UserName;
                UpdatedUser.Password = user.Password;
                UpdatedUser.Address = user.Address;
                UpdatedUser.RoleId = user.RoleId;
                UpdatedUser.RestaurantId = user.RestaurantId;
                UpdatedUser.ManagerId = user.ManagerId;
                await _Context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> ChangePasswordAsync(string userName, string newHashPassword)
        {
            var user = new User
            {
                UserName = userName
            };
            _Context.Attach(user);
            user.Password = newHashPassword;
            int rowsAffected = await _Context.SaveChangesAsync();
            return rowsAffected > 0;

        }
        public async Task<int> AddAsync(User newUser)
        {
            try
            {
                _Context.Users.Add(newUser);
                await _Context.SaveChangesAsync();
                return newUser.Id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                User user = await _Context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    return false;
                }
                _Context.Users.Remove(user);
                await _Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> ExistsAsync(int id)
        {
           return await _Context.Users.AnyAsync(c => c.Id == id);
        }
        public async Task<bool> IsPasswordCorrectAsync(string userName,string hashPassword)
        {
            return await _Context.Users.AnyAsync(c => c.Password == hashPassword && c.UserName==userName);
        }

        public async Task<bool> LoginAsync(string username, string hashPassword)
        {
            return await _Context.Users.AnyAsync(c => c.Password == hashPassword && c.UserName == username);

        }
    }
}
