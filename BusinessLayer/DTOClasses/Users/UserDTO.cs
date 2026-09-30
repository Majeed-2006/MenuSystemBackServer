using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOClasses.Users
{
    public class UserDTO
    {

        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
     
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int RoleId{  get; set; }
        public int RestaurantId { get; set; }
        public int? ManagerId { get; set; }
        public UserDTO(int id, string firstName, string lastName, string userName, string email, string phone, 
                       string address, int roleId, int restaurantId, int? managerId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
           
            Email = email;
            Phone = phone;
            Address = address;
            RoleId = roleId;
            RestaurantId = restaurantId;
            ManagerId = managerId;
        }
    }
}
