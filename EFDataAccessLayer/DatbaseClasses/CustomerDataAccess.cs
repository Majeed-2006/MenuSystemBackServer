
using EFDataAccessLayer.EntityClasses;
using EFDataAccessLayer.SettingClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLayer.DatbaseClasses
{
    public class CustomerDataAccess
    {
        public async static Task<List<Customer>> GetAllCustomers()
        {
            using var context = new EntitiyFrameworkLogic();
            return await context.Customers.ToListAsync();
        }

        public static Customer Find(int id)
        {
            Customer customer = null;
            using var context = new EntitiyFrameworkLogic();
            customer = context.Customers.FirstOrDefault(x=> x.Id == id);
            return customer;
        }

        public static bool Update(Customer customer)
        {
            using var context = new EntitiyFrameworkLogic();
            Customer UpdatedCustomer =  context.Customers.Single(x=> x.Id ==customer.Id);

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
        public static bool UpdateFirstName(Customer customer)
        {
            using var context = new EntitiyFrameworkLogic();
            Customer UpdatedCustomer = context.Customers.Single(x => x.Id == customer.Id);

            if (UpdatedCustomer != null)
            {
                UpdatedCustomer.FirstName = customer.FirstName;
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static bool UpdateLastName(Customer customer)
        {
            using var context = new EntitiyFrameworkLogic();
            Customer UpdatedCustomer = context.Customers.Single(x => x.Id == customer.Id);

            if (UpdatedCustomer != null)
            {
                UpdatedCustomer.LastName = customer.LastName;
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static bool UpdatePhone(Customer customer)
        {
            using var context = new EntitiyFrameworkLogic();
            Customer UpdatedCustomer = context.Customers.Single(x => x.Id == customer.Id);

            if (UpdatedCustomer != null)
            {
                UpdatedCustomer.Phone = customer.Phone;
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static bool UpdateNumberOfOrders(Customer customer)
        {
            using var context = new EntitiyFrameworkLogic();
            Customer UpdatedCustomer = context.Customers.Single(x => x.Id == customer.Id);

            if (UpdatedCustomer != null)
            {
                UpdatedCustomer.NumberOfOrders= customer.NumberOfOrders;
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static bool UpdateEmail(Customer customer)
        {
            using var context = new EntitiyFrameworkLogic();
            Customer UpdatedCustomer = context.Customers.Single(x => x.Id == customer.Id);

            if (UpdatedCustomer != null)
            {
                UpdatedCustomer.Email= customer.Email;
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static bool UpdateLastOrderDate(Customer customer)
        {
            using var context = new EntitiyFrameworkLogic();
            Customer UpdatedCustomer = context.Customers.Single(x => x.Id == customer.Id);

            if (UpdatedCustomer != null)
            {
                UpdatedCustomer.LastOrderDate= customer.LastOrderDate;
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public static int Add(Customer newCustomer)
        {
            try
            {
                using var context = new EntitiyFrameworkLogic();
                context.Customers.Add(newCustomer);
                context.SaveChanges();
                return newCustomer.Id;
            }
            catch (Exception ex)
            {
                return -1;
            }
           
           
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
        public static bool IsExist(int id)
        {
            return true;
        }
    }
}