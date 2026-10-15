using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOClasses
{
    public class RoleDTO
    {
        int Id { get; set; }
        public string Name { get; set; }
        public RoleDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
