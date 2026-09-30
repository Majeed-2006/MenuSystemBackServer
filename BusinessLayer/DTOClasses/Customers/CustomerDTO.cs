using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOClasses.Customers.CustomerDTO
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
        public int RestaurantId { get; set; }
        public CustomerDTO(int id, string firstName, string lastName, string phone, int numberOfOrders, string email,
                           DateTime lastOrderDate,int restaurantId)
        {
            Id  = id;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            NumberOfOrders = numberOfOrders;
            Email = email;
            LastOrderDate = lastOrderDate;
            RestaurantId = restaurantId;
        }

    }
}
