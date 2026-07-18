using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLayer.DTOClasses
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public int OrderID { get; set; }
        public int Quantity { get; set; }
        public int ProductID { get; set; }
        public float UnitPrice { get; set; }
        public OrderItemDTO(int id,int orderId,int qantity,int productId,float unitPrice)
        {
            Id = id;
            OrderID = orderId;     
            Quantity = qantity;
            ProductID = productId;
            UnitPrice = unitPrice; 
        }
    }
}
