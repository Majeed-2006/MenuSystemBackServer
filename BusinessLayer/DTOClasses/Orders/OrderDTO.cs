using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOClasses.Orders
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int CustomerID { get; set; }
        public int CashierID { get; set; }
        public int? WaiterID { get; set; }   
        public int? DriverID { get; set; }
        public double TotalPrice { get; set; }
        public DateTime OrderDateTime { get; set; }
        public int OrderType { get; set; }
        public int OrderStatusID { get; set; }
        public int PaymentStatus {  get; set; }
        public int RestrauntID { get; set; }
        public OrderDTO(int id, int customerID, int cashierID, int? waiterID, int? driverID, double totalPrice, DateTime orderDateTime, int orderType, int orderStatusID, int paymentStatus , int restrauntID)
        {
           Id = id;
           CustomerID = customerID;
           CashierID = cashierID;
           WaiterID = waiterID;
           DriverID = driverID;
           TotalPrice = totalPrice;
           OrderDateTime = orderDateTime;
           OrderType = orderType;
           OrderStatusID = orderStatusID;
           PaymentStatus = paymentStatus;
            RestrauntID = restrauntID;
        }
    }
}
