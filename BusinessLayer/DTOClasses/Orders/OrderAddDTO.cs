using EFDataAccessLayer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOClasses.Orders
{
    public class OrderAddDTO
    {  
        public int CustomerID { get; set; }
        public int CashierID { get; set; }
        public int? WaiterID { get; set; }
        public int? DriverID { get; set; }
        public int RestrauntID { get; set; }
        public double TotalPrice { get; set; }
        public DateTime OrderDateTime { get; set; }
        public OrderType Type { get; set; }
        public int OrderStatusID { get; set; }
        public int PaymentStatus { get; set; }
        public OrderAddDTO( int customerID, int cashierID, int? waiterID, int? driverID,int restrauntID, double totalPrice, DateTime orderDateTime, OrderType orderType, int orderStatusID, int paymentStatus)
        {
            CustomerID = customerID;
            CashierID = cashierID;
            WaiterID = waiterID;
            DriverID = driverID;
            RestrauntID = restrauntID;
            TotalPrice = totalPrice;
            OrderDateTime = orderDateTime;
            Type = orderType;
            OrderStatusID = orderStatusID;
            PaymentStatus = paymentStatus;
        }
    }
}
