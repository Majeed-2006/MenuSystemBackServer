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
        public OrderItemDTO(int id,int orderID, int quantity, int productID, float unitPrice)
        {
            Id = id;
            OrderID = orderID;
            Quantity = quantity;
            ProductID = productID;
            UnitPrice = unitPrice;
        }
    }
}
