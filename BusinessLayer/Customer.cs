using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDataAccessLayer.DatbaseClasses;
using EFDataAccessLayer.DTOClasses;
namespace BusinessLayer
{
    public class Customer
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int NumberOfOrders { get; set; }
        public string Email { get; set; }
        public DateTime LastOrderDate { get; set; }

       public CustomerDTO CDTO
        {
            get
            {
                return (new CustomerDTO(this.Id, this.FirstName, this.LastName, this.Phone, this.NumberOfOrders, this.Email, this.LastOrderDate));
            }
        }

        public Customer(CustomerDTO CDTO, enMode cMode = enMode.AddNew )
        {
            this.Id = CDTO.Id;
            this.FirstName = CDTO.FirstName;
            this.LastName = CDTO.LastName;
            this.Phone = CDTO.Phone;
            this.Email = CDTO.Email;
            this.NumberOfOrders = CDTO.NumberOfOrders;
            this.LastOrderDate = CDTO.LastOrderDate;
            Mode = cMode;
        }
        public static List<CustomerDTO> GetAllCustomers()
        {
            return CustomerDataAccess.GetAllCustomers();
        }
    }
}
