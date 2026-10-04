using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDataAccessLayer.EntityClasses;
using EFDataAccessLayer.SettingClasses;
using Microsoft.EntityFrameworkCore;

namespace EFDataAccessLayer.DatbaseClasses
{
    public class ProductDataAccess
    {
        private readonly AppDbContext _Context;

        public ProductDataAccess(AppDbContext context)
        {
            _Context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            try
            {
                return await _Context.Products.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<Product?> FindAsync(int id, bool isTracking = false)
        {
            if (!isTracking)
                return await _Context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            else
                return await _Context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        // Should have check logic in buiss so u cant update in process order
        public async Task<bool> UpdateAsync(Product product)
        {
            // universal checker or restraunt id based checker -- that is the question 
            Product UpdateProduct= await _Context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);

            if (UpdateProduct != null)
            {
                //open to many changes 
                UpdateProduct.Price = product.Price;
                UpdateProduct.Category= product.Category;
                UpdateProduct.IsAvailable = product.IsAvailable;
                await _Context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<int> AddAsync(Product newProduct)
        {
            try
            {
                _Context.Products.Add(newProduct);
                await _Context.SaveChangesAsync();
                return newProduct.Id;
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
                Product product = await _Context.Products.FirstOrDefaultAsync(x => x.Id == id);
                if (product== null)
                {
                    return false;
                }
                _Context.Products.Remove(product);
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

            return await _Context.Products.AnyAsync(c => c.Id == id);
        }




    }
}
