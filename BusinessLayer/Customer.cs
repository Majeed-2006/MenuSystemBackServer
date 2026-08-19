using EFDataAccessLayer.DatbaseClasses;
using EFDataAccessLayer.DTOClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
namespace BusinessLayer
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int NumberOfOrders { get; set; }
        public string Email { get; set; }
        public DateTime LastOrderDate { get; set; }

        [JsonIgnore]
        public CustomerDTO CDTO
        {
            get
            {
                return (new CustomerDTO(this.Id, this.FirstName, this.LastName, this.Phone, this.NumberOfOrders, this.Email, this.LastOrderDate));
            }
        }

       
        public Customer(CustomerDTO cdto )
        {
            this.Id = cdto.Id;
            this.FirstName = cdto.FirstName;
            this.LastName = cdto.LastName;
            this.Phone = cdto.Phone;
            this.Email = cdto.Email;
            this.NumberOfOrders = cdto.NumberOfOrders;
            this.LastOrderDate = cdto.LastOrderDate;
          
        }
        public static List<CustomerDTO> GetAllCustomers()
        {
            return CustomerDataAccess.GetAllCustomers();
        }
        public bool Add()
        {
            this.Id = CustomerDataAccess.Add(CDTO);
            return this.Id != -1;
        }
       public static Customer Find(int id)
       {
            CustomerDTO customerDTO = CustomerDataAccess.Find(id);
            if (customerDTO != null)
            {
                return new Customer(customerDTO);
            }
            return null;
           
       }
        public bool Update()
        {
            return CustomerDataAccess.Update(CDTO);
        }
        public static bool Delete(int id)
        {
            return CustomerDataAccess.Delete(id);
        }
    }
}
