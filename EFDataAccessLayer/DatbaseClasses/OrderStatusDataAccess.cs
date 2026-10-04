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
    public class OrderStatusDataAccess
    {
        private readonly AppDbContext _Context;

        public OrderStatusDataAccess(AppDbContext context)
        {
            _Context = context;
        }



        public async Task<List<OrderStatus>> GetAllOrderStatusesAsync()
        {
            try
            {
                return await _Context.OrderStatuses.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
