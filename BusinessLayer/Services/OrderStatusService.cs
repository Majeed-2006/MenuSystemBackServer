using App.API.GlobalExceptionHandler.Exceptions;
using BusinessLayer.DTOClasses;
using EFDataAccessLayer.DatbaseClasses;
using EFDataAccessLayer.EntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessLayer.DTOClasses.OrderStatusDTO;
namespace BusinessLayer.Services
{
    public class OrderStatusService
    {
        private readonly OrderStatusDataAccess _OrderStatusDataAccess;

        public OrderStatusService(OrderStatusDataAccess orderStatusDataAccess)
        {
            _OrderStatusDataAccess = orderStatusDataAccess;
        }

        public async Task<List<OrderStatusDTO>> GetAllOrderStatusesAsync()
        {
            var orderStatuses = await _OrderStatusDataAccess.GetAllOrderStatusesAsync();
            if (orderStatuses == null || !orderStatuses.Any())
            {
                throw new BusinessException("empty List");
            }

            return orderStatuses.Select(s => new OrderStatusDTO(
                s.Id,
                s.EngName,
                s.ArName
            )).ToList();
        }

        public async Task<OrderStatusDTO?> FindAsync(int id)
        {
            OrderStatus orderStatus = await _OrderStatusDataAccess.FindAsync(id);
            if (orderStatus != null)
            {
                var orderStatusDTO = new OrderStatusDTO(
                    orderStatus.Id,
                    orderStatus.EngName,
                    orderStatus.ArName
                );
                return orderStatusDTO;
            }
            return null;
        }
    }
}