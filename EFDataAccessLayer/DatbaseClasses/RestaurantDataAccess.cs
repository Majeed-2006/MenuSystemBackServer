using EFDataAccessLayer.SettingClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDataAccessLayer.EntityClasses;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
namespace EFDataAccessLayer.DatbaseClasses
{
    public  class RestaurantDataAccess
    {
        private readonly AppDbContext _Context;
        public RestaurantDataAccess (AppDbContext Context)
        {
            _Context = Context;
        }
        public async Task<List<Restaurant>> GetAllRestaurantsAsync()
        {
            try
            {
                return await _Context.Restaurants.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
               return null;
            }
        }
        public async Task<Restaurant> FindAsync(int id, bool isTracking = false)
        {
            try
            {
                if (!isTracking)

                    return await _Context.Restaurants.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
                else
                    return await _Context.Restaurants.FirstOrDefaultAsync(r => r.Id == id);
            }
            catch
            {
                return null;
            }
        }
        public async Task<int> AddAsync(Restaurant restaurant)
        {
            try
            {
               _Context.Add(restaurant);
                await _Context.SaveChangesAsync();
                return restaurant.Id;

            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public async Task<bool> UpdateAsync(Restaurant newRestaurant)
        {
            try
            {
                var restaurant = await _Context.Restaurants.FirstOrDefaultAsync(r => r.Id == newRestaurant.Id);
                if (restaurant == null)
                    return false;

                restaurant.Name = newRestaurant.Name;
                restaurant.SubDomain = newRestaurant.SubDomain;
                restaurant.CreatedAt = newRestaurant.CreatedAt;
                restaurant.IsDeleted = newRestaurant.IsDeleted;
                await _Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                Restaurant restaurant = await _Context.Restaurants.FirstOrDefaultAsync(r=>r.Id == id);
                if (restaurant == null)
                    return false;

                _Context.Remove(restaurant);
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
            return await _Context.Restaurants.AnyAsync(c => c.Id == id);
        }



    }
}
