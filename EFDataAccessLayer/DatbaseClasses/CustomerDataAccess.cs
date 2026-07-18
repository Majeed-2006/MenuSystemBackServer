using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLayer.DatbaseClasses
{
    public class CustomerDataAccess
    {
        public static List<DTOClasses.CustomerDTO> GetAllCustomers()
        {
            List<DTOClasses.CustomerDTO> customers = new List<DTOClasses.CustomerDTO>();
            return customers;
        }


    }
}
