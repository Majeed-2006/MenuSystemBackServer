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
        public bool IsAvliable { get; set; }    
        public int CreatedByUserID { get; set; }    
        public int CategoryID { get; set; }

        public ProductDTO(int id, string name, string discription, float price, bool isAvilable,int createdByUserId,int categoryId)
        {
            Id = id;
            Name = name;
            Description = discription;
            Price = price;
            IsAvliable = isAvilable;
            CreatedByUserID = createdByUserId;
            CategoryID = categoryId;
        }
    }
}
