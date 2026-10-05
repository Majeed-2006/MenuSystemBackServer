using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOClasses
{
    public class OrderStatusDTO
    {
        public int Id { get; set; }
        public string EngName { get; set; }
        public string ArName { get; set; }
        public OrderStatusDTO(int id,string engName,string arName)
         {
             Id = id;
             EngName = engName;
             ArName = arName;
         }
    }
}
