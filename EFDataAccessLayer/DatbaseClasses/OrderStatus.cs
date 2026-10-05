using EFDataAccessLayer.SettingClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDataAccessLayer.EntityClasses;
using Microsoft.EntityFrameworkCore;

namespace EFDataAccessLayer.DatbaseClasses
{
    public class OrderStatusDataAccess
    {
        private readonly AppDbContext _Context;

        public OrderStatusDataAccess(AppDbContext Context)
        {
            _Context = Context;
        }

        public async Task<List<OrderStatus>> GetAllOrderStatusesAsync()
        {
            try
            {
                return await _Context.OrderStatuses.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<OrderStatus> FindAsync(int id, bool isTracking = false)
        {
            try
            {
                if (!isTracking)
                    return await _Context.OrderStatuses.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
                else
                    return await _Context.OrderStatuses.FirstOrDefaultAsync(s => s.Id == id);
            }
            catch
            {
                return null;
            }
        }
    }
}