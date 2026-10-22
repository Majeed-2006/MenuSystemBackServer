
using BusinessLayer.DTOClasses.Categories;
using BusinessLayer.Exceptions;
using EFDataAccessLayer.DatbaseClasses;
using EFDataAccessLayer.EntityClasses;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EFDataAccessLayer.Enum;
namespace BusinessLayer.Services
{
    public class CategoryService
    {
        private readonly CategoryDataAccess _CategoryDataAccess;
        private readonly UserDataAccess _UserDataAccess;
        private readonly RoleDataAccess _RoleDataAccess;
        public CategoryService(CategoryDataAccess categoryDataAccess,UserDataAccess userDataAccess,RoleDataAccess roleDataAccess)
        {
            _CategoryDataAccess = categoryDataAccess;
            _UserDataAccess = userDataAccess;
            _RoleDataAccess = roleDataAccess;
        }

        Category ConvertToEntity(CategoryDTO categoryDTO)
        {
            Category category = new Category();
            if (categoryDTO != null)
            {
                category.Id = categoryDTO.Id;
                category.Name = categoryDTO.Name;
                category.Description = categoryDTO.Discription;
                category.IsAvailable = categoryDTO.IsAvilable;
                category.PrepDuration = categoryDTO.PrepDuration;
                category.RestaurantId = categoryDTO.RestaurantId;
                category.CreatedByUserId = categoryDTO.CreatedByUserId;
                return category;
            }
            return null;
        }

        Category ConvertToEntity(CategorySaveDTO categorySaveDTO)
        {
            Category category = new Category();
            if (categorySaveDTO != null)
            {
                category.Name = categorySaveDTO.Name;
                category.Description = categorySaveDTO.Discription;
                category.IsAvailable = categorySaveDTO.IsAvilable;
                category.PrepDuration = categorySaveDTO.PrepDuration;
                category.RestaurantId = categorySaveDTO.RestaurantId;
                category.CreatedByUserId = categorySaveDTO.CreatedByUserId;
                return category;
            }
            return null;
        }

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _CategoryDataAccess.GetAllCategoriesAsync();
           
            return categories.Select(c => new CategoryDTO(
                c.Id,
                c.Name,
                c.Description,
                c.IsAvailable,
                c.PrepDuration,
                c.RestaurantId,
                c.CreatedByUserId
            )).ToList();
        }

        public async Task<int> AddAsync(CategorySaveDTO categorySaveDTO)
        {
            Category category = ConvertToEntity(categorySaveDTO);
            List<Role> roles = await _RoleDataAccess.GetAllRolesAsync();
            if (category.CreatedByUserId == roles.FirstOrDefault(r => r.Name == "Admin")?.Id || category.CreatedByUserId == roles.FirstOrDefault(r => r.Name == "Cashier")?.Id)
            {
                var user = await _UserDataAccess.FindAsync(category.CreatedByUserId);
                if (category.RestaurantId == user.RestaurantId)
                    return await _CategoryDataAccess.AddAsync(category);

            }
            return -1;
        }

        public async Task<CategoryDTO> FindAsync(int id)
        {
            Category category = await _CategoryDataAccess.FindAsync(id);
            if (category != null)
            {
                var categoryDTO = new CategoryDTO(
                    category.Id,
                    category.Name,
                    category.Description,
                    category.IsAvailable,
                    category.PrepDuration,
                    category.RestaurantId,
                    category.CreatedByUserId
                );
                return categoryDTO;
            }
            return null;
           
        }

        public async Task<CategorySaveDTO> FindSaveAsync(int id)
        {
            Category category = await _CategoryDataAccess.FindAsync(id, true);
            if (category != null)
            {
                var categorySaveDTO = new CategorySaveDTO(
                    category.Name,
                    category.Description,
                    category.IsAvailable,
                    category.PrepDuration,
                    category.RestaurantId,
                    category.CreatedByUserId
                );
                return categorySaveDTO;
            }
            return null;
        }

        public async Task<bool> UpdateAsync(int id, CategorySaveDTO categorySaveDTO)
        {
            if (await IsExistAsync(id))
            {
                var category = ConvertToEntity(categorySaveDTO);
                List<Role> roles = await _RoleDataAccess.GetAllRolesAsync();
                if (category.CreatedByUserId == roles.FirstOrDefault(r => r.Name == "Admin")?.Id || category.CreatedByUserId == roles.FirstOrDefault(r => r.Name == "Cashier")?.Id)
                {
                    var user = await _UserDataAccess.FindAsync(category.CreatedByUserId);
                    if (category.RestaurantId == user.RestaurantId)
                    {
                        category.Id = id;
                        return await _CategoryDataAccess.UpdateAsync(category);
                    }
                }
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Category category = ConvertToEntity(await FindAsync(id));
            if (category == null)
                return false;
            await _CategoryDataAccess.DeleteAsync(category);
            return true;
               
        }

        public async Task<bool> IsExistAsync(int id)
        {
            return await _CategoryDataAccess.ExistsAsync(id);
        }
    }
}