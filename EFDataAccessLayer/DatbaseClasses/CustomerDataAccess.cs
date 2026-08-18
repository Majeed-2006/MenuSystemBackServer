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
    }
}