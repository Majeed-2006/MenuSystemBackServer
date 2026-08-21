using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using EFDataAccessLayer.DTOClasses;
using EFDataAccessLayer.SettingClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFDataAccessLayer.DatbaseClasses
{
    public class CustomerDataAccess
    {
        public static List<DTOClasses.CustomerDTO> GetAllCustomers()
        {
            using var context = new EntitiyFrameworkLogic();
            return context.Customers.ToList();
        }

        public static CustomerDTO Find(int id)
        {
            CustomerDTO customer = null;
            using var context = new EntitiyFrameworkLogic();
            customer = context.Customers.FirstOrDefault(x=> x.Id == id);
            return customer;
        }

        public static bool Update(CustomerDTO customer)
        {
            using var context = new EntitiyFrameworkLogic();
            CustomerDTO UpdatedCustomer =  context.Customers.Single(x=> x.Id ==customer.Id);

            if (UpdatedCustomer!= null)
            {
                UpdatedCustomer.FirstName = customer.FirstName;
                UpdatedCustomer.LastName = customer.LastName;
                UpdatedCustomer.Email = customer.Email;
                UpdatedCustomer.Phone = customer.Phone;
                UpdatedCustomer.LastOrderDate = customer.LastOrderDate;
                UpdatedCustomer.NumberOfOrders = customer.NumberOfOrders;
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static bool UpdateFirstName(CustomerDTO customer)
        {
            using var context = new EntitiyFrameworkLogic();
            CustomerDTO UpdatedCustomer = context.Customers.Single(x => x.Id == customer.Id);

            if (UpdatedCustomer != null)
            {
                UpdatedCustomer.FirstName = customer.FirstName;
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static bool UpdateLastName(CustomerDTO customer)
        {
            using var context = new EntitiyFrameworkLogic();
            CustomerDTO UpdatedCustomer = context.Customers.Single(x => x.Id == customer.Id);

            if (UpdatedCustomer != null)
            {
                UpdatedCustomer.LastName = customer.LastName;
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static bool UpdatePhone(CustomerDTO customer)
        {
            using var context = new EntitiyFrameworkLogic();
            CustomerDTO UpdatedCustomer = context.Customers.Single(x => x.Id == customer.Id);

            if (UpdatedCustomer != null)
            {
                UpdatedCustomer.Phone = customer.Phone;
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static bool UpdateNumberOfOrders(CustomerDTO customer)
        {
            using var context = new EntitiyFrameworkLogic();
            CustomerDTO UpdatedCustomer = context.Customers.Single(x => x.Id == customer.Id);

            if (UpdatedCustomer != null)
            {
                UpdatedCustomer.NumberOfOrders= customer.NumberOfOrders;
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static bool UpdateEmail(CustomerDTO customer)
        {
            using var context = new EntitiyFrameworkLogic();
            CustomerDTO UpdatedCustomer = context.Customers.Single(x => x.Id == customer.Id);

            if (UpdatedCustomer != null)
            {
                UpdatedCustomer.Email= customer.Email;
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static bool UpdateLastOrderDate(CustomerDTO customer)
        {
            using var context = new EntitiyFrameworkLogic();
            CustomerDTO UpdatedCustomer = context.Customers.Single(x => x.Id == customer.Id);

            if (UpdatedCustomer != null)
            {
                UpdatedCustomer.LastOrderDate= customer.LastOrderDate;
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static int Add(CustomerDTO NewCustomer)
        {
            using var context = new EntitiyFrameworkLogic();
            context.Customers.Add(NewCustomer);
            context.SaveChanges();
            return 1;
        }
        public static bool Delete(int id)
        {
            using var context = new EntitiyFrameworkLogic();
            if (context.Customers.Single(x => x.Id == id) != null)
            {
                return false;
            }
            context.Customers.Remove(context.Customers.Single(x=>x.Id == id));
            context.SaveChanges();
            return true;
        }
    }
}