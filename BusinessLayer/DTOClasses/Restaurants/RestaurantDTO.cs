using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOClasses.Restaurants
{
   public class RestaurantDTO
   {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SubDomain { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public RestaurantDTO(int id,string name, string subDomain, DateTime creatAt, bool isDeleted)
        {
            Id = id;
            Name = name;
            SubDomain = subDomain;
            CreatedAt = creatAt;
            IsDeleted = isDeleted;
        }
    }
}
