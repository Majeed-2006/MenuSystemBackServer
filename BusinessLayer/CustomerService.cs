using App.API.GlobalExceptionHandler.Exceptions;
using BusinessLayer.DTOClasses.Customers;
using BusinessLayer.DTOClasses.Customers.CustomerDTO;
using BusinessLayer.DTOClasses.Customers.CustomerSaveDTO;
using EFDataAccessLayer.DatbaseClasses;
using EFDataAccessLayer.EntityClasses;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class CustomerService
    {
        private Customer _EntityCustomer;
        public CustomerService(CustomerDTO customer) 
        {
           _EntityCustomer = new Customer();
           _EntityCustomer.Id = customer.Id;
           _EntityCustomer.FirstName = customer.FirstName;
           _EntityCustomer.LastName = customer.LastName;
           _EntityCustomer.Email = customer.Email;
           _EntityCustomer.Phone = customer.Phone;
           _EntityCustomer.NumberOfOrders = customer.NumberOfOrders;
           _EntityCustomer.LastOrderDate = customer.LastOrderDate;
        }
        public CustomerService(CustomerSaveDTO customer)
        {
            _EntityCustomer = new Customer();
            _EntityCustomer.FirstName = customer.FirstName;
           _EntityCustomer.LastName = customer.LastName;
           _EntityCustomer.Email = customer.Email;
           _EntityCustomer.Phone = customer.Phone;
           _EntityCustomer.NumberOfOrders = customer.NumberOfOrders;
           _EntityCustomer.LastOrderDate = customer.LastOrderDate;
        }
        public CustomerDTO ConvertToDTO()
        {
            return new CustomerDTO(_EntityCustomer.Id, _EntityCustomer.FirstName, _EntityCustomer.LastName, _EntityCustomer.Phone,
                                   _EntityCustomer.NumberOfOrders, _EntityCustomer.Email, _EntityCustomer.LastOrderDate);
        }
        public async static Task<List<CustomerDTO>> GetAllCustomers()
        {
            var customerDTOs = await CustomerDataAccess.GetAllCustomers();
            if (customerDTOs.ToList().IsNullOrEmpty())
            {
                throw new BusinessException("empty List");
            }

            return customerDTOs.Select(c => new CustomerDTO(

                c.Id,
                c.FirstName,
                c.LastName,
                c.Phone,
                c.NumberOfOrders,
                c.Email,
                c.LastOrderDate

            )).ToList(); ;
        }
        public int Add()
        {
            return CustomerDataAccess.Add(_EntityCustomer); 
        }
        public static CustomerService Find(int id)
        {
            Customer customer = CustomerDataAccess.Find(id);
            if (customer != null)
            {
                var customerDTO = new CustomerDTO(customer.Id, customer.FirstName, customer.LastName, customer.Phone,
                                              customer.NumberOfOrders, customer.Email, customer.LastOrderDate);
                return new CustomerService(customerDTO);

            }
            return null;
           
        }
        public bool Update(CustomerSaveDTO customerDTO)
        {
            if (IsExist(_EntityCustomer.Id))
            {
                _EntityCustomer.FirstName = customerDTO.FirstName;
                _EntityCustomer.LastName = customerDTO.LastName;
                _EntityCustomer.Phone = customerDTO.Phone;
                _EntityCustomer.NumberOfOrders = customerDTO.NumberOfOrders;
                _EntityCustomer.Email = customerDTO.Email;
                _EntityCustomer.LastOrderDate = customerDTO.LastOrderDate;
                return CustomerDataAccess.Update(_EntityCustomer);
            }
            return false;
        }
     
        public static bool Delete(int id)
        {
            return CustomerDataAccess.Delete(id);
        }
        public static bool IsExist(int id)
        {
            return true;
        }
    }
}
