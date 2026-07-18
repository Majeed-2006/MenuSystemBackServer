using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLayer.DTOClasses
{
    public class CustomerDTO
    {
        public int Id { get; set; } 
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Phone {  get; set; }
        public int NumberOfOrders { get; set; }
        public string Email { get; set; }
        public DateTime LastOrderDate { get; set; }

        public CustomerDTO(int id, string firstName, string lastName, string phone, int numberOfOrders, string email, DateTime lastOrderDate)
        {
            Id  = id;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            NumberOfOrders = numberOfOrders;
            Email = email;
            LastOrderDate = lastOrderDate;
        }

    }
}
