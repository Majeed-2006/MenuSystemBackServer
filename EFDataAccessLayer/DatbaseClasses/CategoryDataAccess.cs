using EFDataAccessLayer.EntityClasses;
using EFDataAccessLayer.SettingClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLayer.DatbaseClasses
{
    public class CategoryDataAccess
    {
        private readonly AppDbContext _Context;

        public CategoryDataAccess(AppDbContext context)
        {
            _Context = context;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _Context.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<Category?> FindAsync(int id, bool isTracking = false)
        {
            if (!isTracking)
                return await _Context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            else
                return await _Context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateAsync(Category category)
        {
            Category updatedCategory = await _Context.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);

            if (updatedCategory != null)
            {
                updatedCategory.Name = category.Name;
                updatedCategory.Description = category.Description;
                updatedCategory.IsAvailable = category.IsAvailable;
                updatedCategory.PrepDuration = category.PrepDuration;
                updatedCategory.RestaurantId = category.RestaurantId;
                updatedCategory.CreatedByUserId = category.CreatedByUserId;
                await _Context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateNameAsync(int id, string name)
        {
            var category = new Category
            {
                Id = id
            };

            _Context.Attach(category);
            category.Name = name;
            int rowsAffected = await _Context.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> UpdateDescriptionAsync(int id, string description)
        {
            var category = new Category
            {
                Id = id
            };

            _Context.Attach(category);
            category.Description = description;
            int rowsAffected = await _Context.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> UpdateIsAvailableAsync(int id, bool isAvailable)
        {
            var category = new Category
            {
                Id = id
            };

            _Context.Attach(category);
            category.IsAvailable = isAvailable;
            int rowsAffected = await _Context.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> UpdatePrepDurationAsync(int id, int prepDuration)
        {
            var category = new Category
            {
                Id = id
            };

            _Context.Attach(category);
            category.PrepDuration = prepDuration;
            int rowsAffected = await _Context.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> UpdateRestaurantIdAsync(int id, int restaurantId)
        {
            var category = new Category
            {
                Id = id
            };

            _Context.Attach(category);
            category.RestaurantId = restaurantId;
            int rowsAffected = await _Context.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> UpdateCreatedByUserIdAsync(int id, int createdByUserId)
        {
            var category = new Category
            {
                Id = id
            };

            _Context.Attach(category);
            category.CreatedByUserId = createdByUserId;
            int rowsAffected = await _Context.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<int> AddAsync(Category newCategory)
        {
            try
            {
                _Context.Categories.Add(newCategory);
                await _Context.SaveChangesAsync();
                return newCategory.Id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task DeleteAsync(Category category)
        {
            _Context.Categories.Remove(category);
            await _Context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _Context.Categories.AnyAsync(c => c.Id == id);
        }
    }
}