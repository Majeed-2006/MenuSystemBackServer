using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOClasses.Customers.CustomerSaveDTO
{
    public class CustomerSaveDTO
    {
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int NumberOfOrders { get; set; }
        public string Email { get; set; }
        public DateTime LastOrderDate { get; set; }

        public CustomerSaveDTO(string firstName, string lastName, string phone, int numberOfOrders, string email, DateTime lastOrderDate)
        {
           FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            NumberOfOrders = numberOfOrders;
            Email = email;
            LastOrderDate = lastOrderDate;
        }
    }
}
