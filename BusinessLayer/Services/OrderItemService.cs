using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.API.GlobalExceptionHandler.Exceptions;
using EFDataAccessLayer.DatbaseClasses;
using EFDataAccessLayer.DTOClasses;
using EFDataAccessLayer.EntityClasses;

namespace BusinessLayer.Services
{
    public class OrderItemService
    {
        private readonly OrderItemDataAccess _OrderItemDataAccess;
        public OrderItemService(OrderItemDataAccess OrderItemDataAccess)
        {
            _OrderItemDataAccess = OrderItemDataAccess;
        }



        OrderItem ConvertToEntity(OrderItemDTO OrderItemDTO)
        {

            OrderItem orderItem= new OrderItem();
            if (orderItem!= null)
            {
                orderItem.Id = OrderItemDTO.Id;
                orderItem.OrderId = OrderItemDTO.OrderID;
                orderItem.Quantity = OrderItemDTO.Quantity;
                orderItem.ProductId = OrderItemDTO.ProductID;
                orderItem.UnitPrice= OrderItemDTO.UnitPrice;

                return orderItem;
            }
            return null;
        }

        public async Task<List<OrderItemDTO>> GetAllOrderItemsAsync()
        {
            var orderItems = await _OrderItemDataAccess.GetAllOrderItemsAsync();
            if (orderItems == null || !orderItems.Any())
            {
                throw new BusinessException("empty List");
            }

            return orderItems.Select(o => new OrderItemDTO(

                o.Id,
                o.OrderId,
                o.Quantity,
                o.ProductId,
                o.UnitPrice
               
            )).ToList();
        }

    }
}
