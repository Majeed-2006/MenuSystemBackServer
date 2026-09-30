using App.API.GlobalExceptionHandler.Exceptions;
using BusinessLayer.DTOClasses.Users;
using EFDataAccessLayer.DatbaseClasses;
using EFDataAccessLayer.EntityClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EFDataAccessLayer.DatbaseClasses.UserDataAccess;
using static BusinessLayer.Utitlity.Hashing;
namespace BusinessLayer.Services
{
    public class UserService
    {
        private readonly UserDataAccess _UserDataAccess;
        public UserService(UserDataAccess userDataAccess)
        {
            _UserDataAccess = userDataAccess;
        }
      
        User ConvertToEntity(UserSaveDTO userSaveDTO)
        {

            User user = new User();
            if (userSaveDTO != null)
            {
                user.FirstName = userSaveDTO.FirstName;
                user.LastName = userSaveDTO.LastName;
                user.Email = userSaveDTO.Email;
                user.Phone = userSaveDTO.Phone;
                user.UserName = userSaveDTO.UserName;
                user.Address = userSaveDTO.Address;
                user.RoleId = userSaveDTO.RoleId;
                user.RestaurantId = userSaveDTO.RestaurantId;
                user.ManagerId = userSaveDTO.ManagerId;

                return user;
            }
            return null;
        }
        User ConvertToEntity(UserAddDTO userAddDTO)
        {

            User user = new User();
            if (userAddDTO != null)
            {
                user.FirstName = userAddDTO.FirstName;
                user.LastName = userAddDTO.LastName;
                user.Email = userAddDTO.Email;
                user.Phone = userAddDTO.Phone;
                user.UserName = userAddDTO.UserName;
                user.Address = userAddDTO.Address;
                user.Password = userAddDTO.Password;
                user.RoleId = userAddDTO.RoleId;
                user.RestaurantId = userAddDTO.RestaurantId;
                user.ManagerId = userAddDTO.ManagerId;

                return user;
            }
            return null;
        }
        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            var users = await _UserDataAccess.GetAllUsersAsync();
            if (users==null || !users.Any())
            {
                throw new BusinessException("empty List");
            }

            return users.Select( u=> new UserDTO(

                u.Id,
                u.FirstName,
                u.LastName,
                u.UserName,
                u.Phone,
                u.Address,
                u.Email,
                u.RoleId,
                u.RestaurantId,
                u.ManagerId

            )).ToList();
        }
        public async Task<int> AddAsync(UserAddDTO userSaveDTO)
        {
            User user= ConvertToEntity(userSaveDTO);
            return await _UserDataAccess.AddAsync(user);
        }
        public async Task<UserDTO> FindAsync(int id)
        {
            User user = await _UserDataAccess.FindAsync(id);
            if (user != null)
            {
                var userDTO = new UserDTO(user.Id,user.FirstName, user.LastName, user.UserName, user.Email, 
                                          user.Phone, user.Address, user.RoleId, user.RestaurantId,user.ManagerId);
                return userDTO;

            }
            return null;

        }
        public async Task<UserSaveDTO> FindSaveAsync(int id)
        {
            User user = await _UserDataAccess.FindAsync(id,true);
            if (user != null)
            {
                var userDTO = new UserSaveDTO(user.FirstName, user.LastName, user.UserName, user.Email,
                                          user.Phone, user.Address, user.RoleId, user.RestaurantId, user.ManagerId);
                return userDTO;

            }
            return null;

        }
        public async Task<bool> UpdateAsync(int id, UserSaveDTO userSaveDTO)
        {
            if (await IsExistAsync(id))
            {
                User user =await _UserDataAccess.FindAsync(id);
                user.FirstName = userSaveDTO.FirstName;
                user.LastName = userSaveDTO.LastName;
                user.Email = userSaveDTO.Email;
                user.Phone = userSaveDTO.Phone;
                user.UserName = userSaveDTO.UserName;
                user.Address = userSaveDTO.Address;
                user.RoleId = userSaveDTO.RoleId;
                user.RestaurantId = userSaveDTO.RestaurantId;
                user.ManagerId = userSaveDTO.ManagerId;
                return await _UserDataAccess.UpdateAsync(user);
            }
            return false;
        }
        public async Task<bool> ChangePasswordAsync(UserChangePasswordDTO userChangePasswordDTO)
        {
            if ( await IsPasswordCorrectAsync(userChangePasswordDTO.UserName, userChangePasswordDTO.CurrentPassword) )
            {
                return await _UserDataAccess.ChangePasswordAsync(userChangePasswordDTO.UserName, Encrypt(userChangePasswordDTO.NewPassword));
            }
            return false;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _UserDataAccess.DeleteAsync(id);
        }
        public async Task<bool> IsExistAsync(int id)
        {
            return await _UserDataAccess.ExistsAsync(id);
        }
        public async Task<bool> IsPasswordCorrectAsync(string userName,string Password)
        {
            return await _UserDataAccess.IsPasswordCorrectAsync(userName,Encrypt(Password));
        }
        public async Task<bool> LoginAsync(UserLoginDTO userLoginDTO)
        {
            if ( await _UserDataAccess.LoginAsync(userLoginDTO.UserName, Encrypt(userLoginDTO.Password)))
                return true;
            else
                return false;
        }
    }
}
