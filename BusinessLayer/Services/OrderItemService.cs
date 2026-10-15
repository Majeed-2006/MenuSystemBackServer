
using BusinessLayer.DTOClasses.OrderItems;
using EFDataAccessLayer.DatbaseClasses;
using EFDataAccessLayer.EntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class OrderItemService
    {
        private readonly OrderItemDataAccess _OrderItemDataAccess;
        private readonly OrderDataAccess _OrderDataAccess;
        private readonly ProductDataAccess _ProductDataAccess;
        public OrderItemService(OrderItemDataAccess orderItemDataAccess, OrderDataAccess orderDataAccess,
                                ProductDataAccess productDataAccess)
        {
            _OrderItemDataAccess = orderItemDataAccess;
            _OrderDataAccess = orderDataAccess;
            _ProductDataAccess = productDataAccess;
        }

        OrderItem ConvertToEntity(OrderItemDTO orderItemDTO)
        {
            OrderItem orderItem = new OrderItem();
            if (orderItemDTO != null)
            {
                orderItem.Id = orderItemDTO.Id;
                orderItem.Quantity = orderItemDTO.Quantity;
                orderItem.UnitPrice = orderItemDTO.UnitPrice;
                orderItem.OrderId = orderItemDTO.OrderID;
                orderItem.ProductId = orderItemDTO.ProductID;
                return orderItem;
            }
            return null;
        }

        OrderItem ConvertToEntity(OrderItemSaveDTO orderItemSaveDTO)
        {
            OrderItem orderItem = new OrderItem();
            if (orderItemSaveDTO != null)
            {
                orderItem.Quantity = orderItemSaveDTO.Quantity;
                orderItem.UnitPrice = orderItemSaveDTO.UnitPrice;
                orderItem.OrderId = orderItemSaveDTO.OrderID;
                orderItem.ProductId = orderItemSaveDTO.ProductID;
                return orderItem;
            }
            return null;
        }

        public async Task<List<OrderItemDTO>> GetAllOrderItemsAsync()
        {
            var orderItems = await _OrderItemDataAccess.GetAllOrderItemsAsync();
            if (orderItems == null || !orderItems.Any())
            {
               // throw new BusinessException("empty List");
            }

            return orderItems.Select(o => new OrderItemDTO(
                o.Id,
                o.OrderId,
                o.Quantity,
                o.ProductId,
                o.UnitPrice
            )).ToList();
        }

        public async Task<int> AddAsync(OrderItemSaveDTO orderItemDTO)
        {
            OrderItem orderItem = ConvertToEntity(orderItemDTO);
            if (orderItem.Order.RestaurantId == orderItem.Product.RestaurantId)
                return await _OrderItemDataAccess.AddAsync(orderItem);
            else
                return - 1;
        }

        public async Task<OrderItemDTO> FindAsync(int id)
        {
            OrderItem orderItem = await _OrderItemDataAccess.FindAsync(id);
            if (orderItem != null)
            {
                var orderItemDTO = new OrderItemDTO(
                    orderItem.Id,
                    orderItem.OrderId,
                    orderItem.Quantity,
                    orderItem.ProductId,
                    orderItem.UnitPrice
                );
                return orderItemDTO;
            }
            return null;
        }

        public async Task<OrderItemSaveDTO> FindSaveAsync(int id)
        {
            OrderItem orderItem = await _OrderItemDataAccess.FindAsync(id, true);
            if (orderItem != null)
            {
                var orderItemSaveDTO = new OrderItemSaveDTO(
                    orderItem.OrderId,
                    orderItem.Quantity,
                    orderItem.ProductId,
                    orderItem.UnitPrice
                );
                return orderItemSaveDTO;
            }
            return null;
        }

        public async Task<bool> UpdateAsync(int id, OrderItemSaveDTO orderItemDTO)
        {
            if (await IsExistAsync(id))
            {
                var orderItem = ConvertToEntity(orderItemDTO);
                orderItem.Id = id;
                if (orderItem.Order.RestaurantId == orderItem.Product.RestaurantId)
                    return await _OrderItemDataAccess.UpdateAsync(orderItem);
                return false;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _OrderItemDataAccess.DeleteAsync(id);
        }

        public async Task<bool> IsExistAsync(int id)
        {
            return await _OrderItemDataAccess.ExistsAsync(id);
        }
    }
}