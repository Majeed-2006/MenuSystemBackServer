using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessLayer.DTOClasses.Customers.CustomerDTO;
using BusinessLayer.DTOClasses.Customers.CustomerSaveDTO;
using BusinessLayer.DTOClasses.Orders;
using EFDataAccessLayer.DatbaseClasses;
using EFDataAccessLayer.EntityClasses;

namespace BusinessLayer.Services
{
    public class OrderService
    {
        private readonly OrderDataAccess _OrderDataAccess;
        private readonly RestaurantDataAccess _RestaurantDataAccess;
        private readonly CustomerDataAccess _CustomerDataAccess;
        private readonly UserDataAccess _UserDataAccess;
        public OrderService(OrderDataAccess OrderDataAccess,RestaurantDataAccess restaurantDataAccess,
                            UserDataAccess userDataAccess,CustomerDataAccess customerDataAccess)
        {
            _OrderDataAccess = OrderDataAccess;
            _RestaurantDataAccess = restaurantDataAccess;
            _CustomerDataAccess = customerDataAccess;
            _UserDataAccess = userDataAccess;

        }

        Order ConvertToEntity(OrderSaveDTO orderSaveDTO)
        {

            Order order = new Order();
            if (orderSaveDTO!= null)
            {
                order.TotalPrice = orderSaveDTO.TotalPrice;
                return order;
            }
            return null;
        }
        Order ConvertToEntity(OrderAddDTO orderAddDTO)
        {
            Order order = new Order();
            {
                if (orderAddDTO != null)
                {
                    order.CashierId = orderAddDTO.CashierID;
                    order.WaiterId= orderAddDTO.WaiterID;
                    order.DriverId = orderAddDTO.DriverID;
                    order.CustomerId= orderAddDTO.CustomerID;
                    order.PaymentStatus= orderAddDTO.PaymentStatus;
                    order.TotalPrice= orderAddDTO.TotalPrice;
                    order.OrderStatusId= orderAddDTO.OrderStatusID;
                    order.Type= orderAddDTO.Type;
                    order.OrderDateTime = orderAddDTO.OrderDateTime;
                    return order;
                }
                return null;
            }
        }



        public async Task<List<OrderDTO>> GetAllOrdersAsync()
        {
            var orders = await _OrderDataAccess.GetAllOrdersAsync();
            if (orders == null || !orders.Any())
            {
               // throw new BusinessException("empty List");
            }

            return orders.Select(c => new OrderDTO(

                c.Id,
                c.CustomerId,
                c.CashierId,
                c.WaiterId,
                c.DriverId,
                c.TotalPrice,
                c.OrderDateTime,
                c.Type,
                c.OrderStatusId,
                c.PaymentStatus,
                c.RestaurantId
            )).ToList();
        }



        public async Task<int> AddAsync(OrderAddDTO orderDTO)
        {
            Order order = ConvertToEntity(orderDTO);

            return await _OrderDataAccess.AddAsync(order);
        }
        public async Task<OrderAddDTO> FindAsync(int id)
        {
            Order order = await _OrderDataAccess.FindAsync(id);
            if (order != null)
            {
                var orderDTO= new OrderAddDTO(order.CustomerId,order.CashierId,order.WaiterId,order.DriverId,order.TotalPrice,
                                           order.OrderDateTime,order.Type,order.OrderStatusId,order.PaymentStatus);
                return orderDTO;

            }
            return null;

        }


        public async Task<bool> UpdateAsync(int id, OrderSaveDTO orderSaveDTO)
        {
            if (await IsExistAsync(id))
            {
                var order= new Order
                {
                    //shouldnt be id here but we have for code rn 
                    Id = id,
                    TotalPrice = orderSaveDTO.TotalPrice,
                  
                };

                return await _OrderDataAccess.UpdateAsync(order);
            }
            return false;
        }
        //user shouldnt know about their id so were inpytting the number of the user and the system makes a seprate check for finding user
        public async Task <bool> UpdateAsyncOffline(int id, OrderDTO orderDTO)
        {
            // Switch the id with the number later
            if (await IsExistAsync(id))
            {
                var order = new Order
                {
                    Id = id,
                    OrderDateTime = orderDTO.OrderDateTime,
                    PaymentStatus = orderDTO.PaymentStatus,
                    DriverId = orderDTO.DriverID,
                    CustomerId = orderDTO.CustomerID,
                    WaiterId = orderDTO.WaiterID,
                    CashierId = orderDTO.CashierID,
                    TotalPrice = orderDTO.TotalPrice,
                    Type = orderDTO.Type,
                    OrderStatusId = orderDTO.OrderStatusID,
                    RestaurantId = orderDTO.RestrauntID
                };
                return await _OrderDataAccess.UpdateAsync(order);
            }
            return false;
        }



        public async Task<bool> DeleteAsync(int id)
        {
            return await _OrderDataAccess.DeleteAsync(id);
        }
        public async Task<bool> IsExistAsync(int id)
        {
            return await _OrderDataAccess.ExistsAsync(id);
        }



    }
}