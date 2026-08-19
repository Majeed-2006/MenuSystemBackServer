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
            return customer;
        }

        public static bool Update(CustomerDTO customer)
        {
            return false;
        }
        public static int Add(CustomerDTO customer)
        {
            return -1;
        }
        public static bool Delete(int id)
        {
            return false;
        }
    }
}