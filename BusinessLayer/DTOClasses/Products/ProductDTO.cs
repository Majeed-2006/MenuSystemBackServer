using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOClasses.Products
{
   public class ProductDTO
    {


        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public bool IsAvilable { get; set; }    
        public int CreatedByUserID { get; set; }
        public int CreatedByRestrauntID { get; set; }
        public int CategoryID { get; set; }

        public ProductDTO(int id, string name, string description, double price, bool isAvilable,int createdByUserID ,  int createdByRestrauntID, int categoryID)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            IsAvilable = isAvilable;
            CreatedByUserID = createdByUserID;
            CreatedByRestrauntID = createdByRestrauntID;
            CategoryID = categoryID;
        }
    }
}
