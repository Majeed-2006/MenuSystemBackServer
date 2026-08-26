using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLayer.DTOClasses
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int CustomerID { get; set; }
        public int CashierID { get; set; }
        public int WaiterID { get; set; }   
        public int DriverID { get; set; }
        public float TotalPrice { get; set; }
        public DateTime OrderDateTime { get; set; }
        public int OrderrType { get; set; }
        public int OrderStatusID { get; set; }
        public int PaymentStatus {  get; set; }
        public OrderDTO(int id, int customerID, int cashierID, int waiterID, int driverID, float totalPrice, DateTime orderDateTime, int orderrType, int orderStatusID, int paymentStatus)
        {
           Id = id;
           CustomerID = customerID;
           CashierID = cashierID;
           WaiterID = waiterID;
           DriverID = driverID;
           TotalPrice = totalPrice;
           OrderDateTime = orderDateTime;
           OrderrType = orderrType;
           OrderStatusID = orderStatusID;
           PaymentStatus = paymentStatus;
        }
    }
}
