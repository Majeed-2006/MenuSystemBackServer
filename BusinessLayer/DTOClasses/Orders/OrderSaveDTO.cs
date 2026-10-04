using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOClasses.Orders
{
    public class OrderSaveDTO
    {
        public double TotalPrice { get; set; }
        public DateTime OrderDateTime { get; set; }
        public int OrderType { get; set; }
        public int PaymentStatus { get; set; }

        public OrderSaveDTO(double totalPrice , DateTime orderDateTime)
        {
            TotalPrice = totalPrice;
            OrderDateTime = orderDateTime;
        }
    }
}
