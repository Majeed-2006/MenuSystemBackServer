using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessLayer.DTOClasses.Orders;
using BusinessLayer.DTOClasses.Products;
using BusinessLayer.DTOClasses.Users;
using EFDataAccessLayer.DatbaseClasses;
using EFDataAccessLayer.EntityClasses;

namespace BusinessLayer.Services
{
    public class ProductService
    {
        private readonly ProductDataAccess _ProductDataAccess;
        private readonly UserDataAccess _UserDataAccess;

        public ProductService(ProductDataAccess productDataAccess, UserDataAccess userDataAccess)
        {
            _ProductDataAccess= productDataAccess;
            _UserDataAccess = userDataAccess;
        }
        Product ConvertToEntity(ProductDTO productDTO)
        {
            Product product = new Product();
            {
                if (productDTO!= null)
                {
                    product.Name = productDTO.Name;
                    product.CreatedByUserId= productDTO.CreatedByUserID;
                    product.RestaurantId = productDTO.CreatedByRestrauntID;
                    product.Price= productDTO.Price;
                    product.CategoryId= productDTO.CategoryID;
                    product.Description= productDTO.Description;
                    product.IsAvailable = productDTO.IsAvilable;
                    return product;
                }
                return null;
            }
        }
        public async Task<List<ProductDTO>> GetAllProductsAsync()
        {
            var products= await _ProductDataAccess.GetAllProductsAsync();
            if (products== null || !products.Any())
            {
              //  throw new BusinessException("empty List");
            }

            return products.Select(p=> new ProductDTO(
                p.Id,
                p.Name,
                p.Description,
                p.Price,
                p.IsAvailable,
                p.CreatedByUserId,
                p.RestaurantId,
                p.CategoryId
            )).ToList();
        }

        public async Task<int> AddAsync(ProductDTO productDTO)
        {
            Product product = ConvertToEntity(productDTO);
            if (product.CreatedByUserId == 2 || product.CreatedByUserId == 1)
            {
                var user = await _UserDataAccess.FindAsync(product.CreatedByUserId);
                if (product.RestaurantId == user.RestaurantId)
                    return await _ProductDataAccess.AddAsync(product);

            }
            return -1;
         
        }

        public async Task<bool> UpdateAsync(int id , ProductDTO productDTO)
        {
            if (await IsExistAsync(id))
            {
                Product product = ConvertToEntity(productDTO);
                if (product.CreatedByUserId == 2 || product.CreatedByUserId == 1)
                {
                    var user = await _UserDataAccess.FindAsync(product.CreatedByUserId);
                    if (product.RestaurantId == user.RestaurantId)
                    {
                        product.Id = id;
                        return await _ProductDataAccess.UpdateAsync(product);
                    }
                }
            }
            return false;
        }

        public async Task<ProductDTO> FindAsync(int id)
        {
            Product Product = await _ProductDataAccess.FindAsync(id);
            if (Product != null)
            {
                var productDTO = new ProductDTO(
                    Product.Id,
                    Product.Description,
                    Product.Name,
                    Product.Price,
                    Product.IsAvailable,
                    Product.CreatedByUserId,
                    Product.RestaurantId,
                    Product.CategoryId
                );


                return productDTO;

            }
            return null;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (FindAsync(id) == null)
            {
                return false;
            }
            return await _ProductDataAccess.DeleteAsync(id);

        }
        public async Task<bool> IsExistAsync(int id)
        {
            return await _ProductDataAccess.ExistsAsync(id);
        }


    }
}
