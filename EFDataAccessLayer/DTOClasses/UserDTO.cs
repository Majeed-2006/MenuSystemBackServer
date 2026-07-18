using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLayer.DTOClasses
{
    public class UserDTO
    {

        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UseName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int Role {  get; set; }

        public UserDTO(int id, string firstName, string lastName, string useName, string password, string email, string phone, string address, int role)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            UseName = useName;
            Password = password;
            Email = email;
            Phone = phone;
            Address = address;
            Role = role;
        }
    }
}
