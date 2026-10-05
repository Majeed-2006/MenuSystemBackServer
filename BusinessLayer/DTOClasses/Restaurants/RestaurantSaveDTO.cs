using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOClasses.Restaurants
{
    public class RestaurantSaveDTO
    {
        public string Name { get; set; } = null!;
        public string SubDomain { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public RestaurantSaveDTO(string name, string subDomain,DateTime creatAt,bool isDeleted)
        { 
            Name = name;
            SubDomain = subDomain;
            CreatedAt = creatAt;
            IsDeleted = isDeleted;
        }

    }
}
