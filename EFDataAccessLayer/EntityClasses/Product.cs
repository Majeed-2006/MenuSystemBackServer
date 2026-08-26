using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLayer.DTOClasses
{
   public class Product
    {


        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public bool IsAvilable { get; set; }    
        public int CreatedByUserID { get; set; }    
        public int CategoryID { get; set; }

        
    }
}
