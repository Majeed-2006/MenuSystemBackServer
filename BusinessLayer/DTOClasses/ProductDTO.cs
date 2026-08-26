using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLayer.DTOClasses
{
   public class ProductDTO
    {


        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public bool IsAvilable { get; set; }    
        public int CreatedByUserID { get; set; }    
        public int CategoryID { get; set; }

        public ProductDTO(int id, string name, string description, float price, bool isAvilable,int createdByUserID, int categoryID)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            IsAvilable = isAvilable;
            CreatedByUserID = createdByUserID;
            CategoryID = categoryID;
        }
    }
}
