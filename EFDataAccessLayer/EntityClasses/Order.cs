using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLayer.DTOClasses
{
    public class Order
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
       
    }
}
