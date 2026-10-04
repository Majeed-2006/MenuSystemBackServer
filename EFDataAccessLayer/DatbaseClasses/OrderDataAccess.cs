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
    public class OrderDataAccess
    {
        private readonly AppDbContext _Context;

        public OrderDataAccess(AppDbContext context)
        {
            _Context = context;
        }
        //should have multible versions 
        // EX; specfic branch // Person // ALL in general
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            try
            {
                return await _Context.Orders.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<Order?> FindAsync(int id, bool isTracking = false)
        {
            if (!isTracking)
                return await _Context.Orders.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            else
                return await _Context.Orders.FirstOrDefaultAsync(x => x.Id == id);
        }

        // Should have check logic in buiss so u cant update in process order
        public async Task<bool> UpdateAsync(Order order)
        {
            Order UpdatedOrder = await _Context.Orders.FirstOrDefaultAsync(x => x.Id == order.Id);

            if (UpdatedOrder != null)
            {
                //open to many changes 
                UpdatedOrder.TotalPrice = order.TotalPrice;
                UpdatedOrder.OrderDateTime = order.OrderDateTime;
                await _Context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<int> AddAsync(Order newOrder)
        {
            try
            {
                _Context.Orders.Add(newOrder);
                await _Context.SaveChangesAsync();
                return newOrder.Id;
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
                Order order =  await _Context.Orders.FirstOrDefaultAsync(x => x.Id == id);
                if (order== null)
                {
                    return false;
                }
                _Context.Orders.Remove(order);
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

            return await _Context.Orders.AnyAsync(c => c.Id == id);
        }

    }
}
