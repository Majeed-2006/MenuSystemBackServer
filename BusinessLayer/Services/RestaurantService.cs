using App.API.GlobalExceptionHandler.Exceptions;
using BusinessLayer.DTOClasses.Restaurants;
using  BusinessLayer.DTOClasses.Restaurants;
using BusinessLayer.DTOClasses.Restaurants;
using EFDataAccessLayer.DatbaseClasses;
using EFDataAccessLayer.EntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class RestaurantService
    {
        private readonly RestaurantDataAccess _RestaurantDataAccess;
        public RestaurantService(RestaurantDataAccess restaurantDataAccess) 
        { 
            _RestaurantDataAccess = restaurantDataAccess;
        }
        public Restaurant ConvertToEntity(RestaurantDTO restarantDTO)
        {
            var restaaurant = new Restaurant
            {
                Id = restarantDTO.Id,
                Name = restarantDTO.Name,
                SubDomain = restarantDTO.SubDomain,
                CreatedAt = restarantDTO.CreatedAt,
                IsDeleted = restarantDTO.IsDeleted,
            };
            return restaaurant; 
        }

        public Restaurant ConvertToEntity(RestaurantSaveDTO restarantDTO)
        {
            var restaaurant = new Restaurant
            {
                Name = restarantDTO.Name,
                SubDomain = restarantDTO.SubDomain,
                CreatedAt = restarantDTO.CreatedAt,
                IsDeleted = restarantDTO.IsDeleted,
            };
            return restaaurant;
        }

        public async Task<List<RestaurantDTO>> GetAllRestaurantsAsync()
        {
            var Restaurants = await _RestaurantDataAccess.GetAllRestaurantsAsync();
            if (Restaurants == null || !Restaurants.Any())
            {
                throw new BusinessException("empty List");
            }

            return Restaurants.Select(r => new RestaurantDTO(

                r.Id,
                r.Name,
                r.SubDomain,
                r.CreatedAt,
                r.IsDeleted
            )).ToList();
        }

        public async Task<int> AddAsync(RestaurantSaveDTO RestaurantDTO)
        {
            Restaurant Restaurant = ConvertToEntity(RestaurantDTO);
            return await _RestaurantDataAccess.AddAsync(Restaurant);
        }

        public async Task<RestaurantDTO> FindAsync(int id)
        {
            Restaurant Restaurant = await _RestaurantDataAccess.FindAsync(id);
            if (Restaurant != null)
            {
                var RestaurantDTO = new RestaurantDTO(
                    Restaurant.Id,
                    Restaurant.Name,
                    Restaurant.SubDomain,
                    Restaurant.CreatedAt,
                    Restaurant.IsDeleted
                );
                return RestaurantDTO;
            }
            return null;
        }

        public async Task<RestaurantSaveDTO> FindSaveAsync(int id)
        {
            Restaurant Restaurant = await _RestaurantDataAccess.FindAsync(id, true);
            if (Restaurant != null)
            {
                var RestaurantSaveDTO = new RestaurantSaveDTO(
                    Restaurant.Name,
                    Restaurant.SubDomain,
                    Restaurant.CreatedAt,
                    Restaurant.IsDeleted
                );
                return RestaurantSaveDTO;
            }
            return null;
        }

        public async Task<bool> UpdateAsync(int id, RestaurantSaveDTO RestaurantDTO)
        {
            if (await IsExistAsync(id))
            {
                var Restaurant = ConvertToEntity(RestaurantDTO);
                return await _RestaurantDataAccess.UpdateAsync(Restaurant);
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _RestaurantDataAccess.DeleteAsync(id);
        }

        public async Task<bool> IsExistAsync(int id)
        {
            return await _RestaurantDataAccess.ExistsAsync(id);
        }

    }
}
