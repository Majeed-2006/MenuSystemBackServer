using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.API.GlobalExceptionHandler.Exceptions;
using BusinessLayer.DTOClasses.Users;
using EFDataAccessLayer.DatbaseClasses;
using EFDataAccessLayer.DTOClasses;
using EFDataAccessLayer.EntityClasses;

namespace BusinessLayer.Services
{
    public class OrderStatusService
    {
        private readonly OrderStatusDataAccess _OrderStatusDataAccess;
        public OrderStatusService(OrderStatusDataAccess OrderStatusDataAccess)
        {
            _OrderStatusDataAccess= OrderStatusDataAccess;
        }


        OrderStatus ConvertToEntity(OrderStatusDTO OrderStatusDTO)
        {

            OrderStatus orderstatus= new OrderStatus(); 
            if (OrderStatusDTO != null)
            {
                orderstatus.Id = OrderStatusDTO.Id;
                orderstatus.EngName = OrderStatusDTO.EngName;
                orderstatus.ArName = OrderStatusDTO.ArName;
                

                return orderstatus;
            }
            return null;
        }



        public async Task<List<OrderStatusDTO>> GetAllOrderStatusesAsync()
        {
            var orderStatus= await _OrderStatusDataAccess.GetAllOrderStatusesAsync();
            if (orderStatus == null || !orderStatus.Any())
            {
                throw new BusinessException("empty List");
            }

            return orderStatus.Select(o=> new OrderStatusDTO(

                o.Id,
                o.EngName,
                o.ArName

            )).ToList();
        }
    }
}
