using EFDataAccessLayer.EntityClasses;
using EFDataAccessLayer.SettingClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<OrderItem?> FindAsync(int id, bool isTracking = false)
        {
            if (!isTracking)
                return await _Context.OrderItems.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            else
                return await _Context.OrderItems.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateAsync(OrderItem orderItem)
        {
            OrderItem UpdatedOrderItem = await _Context.OrderItems.FirstOrDefaultAsync(x => x.Id == orderItem.Id);

            if (UpdatedOrderItem != null)
            {
                UpdatedOrderItem.Quantity = orderItem.Quantity;
                UpdatedOrderItem.UnitPrice = orderItem.UnitPrice;
                UpdatedOrderItem.OrderId = orderItem.OrderId;
                UpdatedOrderItem.ProductId = orderItem.ProductId;

                await _Context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<int> AddAsync(OrderItem newOrderItem)
        {
            try
            {
                _Context.OrderItems.Add(newOrderItem);
                await _Context.SaveChangesAsync();
                return newOrderItem.Id;
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
                OrderItem orderItem = await _Context.OrderItems.FirstOrDefaultAsync(x => x.Id == id);
                if (orderItem == null)
                {
                    return false;
                }
                _Context.OrderItems.Remove(orderItem);
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
            return await _Context.OrderItems.AnyAsync(o => o.Id == id);
        }
    }
}