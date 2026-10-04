using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.API.GlobalExceptionHandler.Exceptions;
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

        public ProductService(ProductDataAccess productDataAccess)
        {
            _ProductDataAccess= productDataAccess;
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
                throw new BusinessException("empty List");
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
            return await _ProductDataAccess.AddAsync(product);
        }

    }
}
