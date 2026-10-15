using EFDataAccessLayer.Enum;
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
        public OrderType Type { get; set; }
        public int PaymentStatus { get; set; }
        public int OderStatusID { get; set; }

        public OrderSaveDTO(double totalPrice , OrderType orderType, int oderStatusID, int paymentStatus)
        {
            TotalPrice = totalPrice;
            OderStatusID = oderStatusID;
            PaymentStatus = paymentStatus;
            Type = orderType;
        }
    }
}
