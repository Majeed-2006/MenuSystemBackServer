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
    public class OrderItemDataAccess
    {
        private readonly AppDbContext _Context;

        public OrderItemDataAccess(AppDbContext context)
        {
            _Context = context;
        }


        public async Task<List<OrderItem>> GetAllOrderItemsAsync()
        {
            try
            {
                return await _Context.OrderItems.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
