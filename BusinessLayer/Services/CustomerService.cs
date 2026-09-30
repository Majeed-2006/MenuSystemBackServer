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

namespace BusinessLayer.Services
{
    public class CustomerService
    {
      
        private readonly CustomerDataAccess _CustomerDataAccess;
        public CustomerService(CustomerDataAccess CustomerDataAccess) 
        {
           _CustomerDataAccess = CustomerDataAccess;
        }

        Customer ConvertToEntity(CustomerDTO CustomerDTO)
        {
            
            Customer Customer= new Customer();
            if (CustomerDTO != null)
            {
                Customer.Id = CustomerDTO.Id;
                Customer.FirstName = CustomerDTO.FirstName;
                Customer.LastName = CustomerDTO.LastName;
                Customer.Phone = CustomerDTO.Phone;
                Customer.NumberOfOrders = CustomerDTO.NumberOfOrders;
                Customer.Email = CustomerDTO.Email;
                Customer.LastOrderDate = CustomerDTO.LastOrderDate;
                Customer.RestaurantId = CustomerDTO.RestaurantId;
                return Customer;
            }
            return null;
        }
        Customer ConvertToEntity(CustomerSaveDTO CustomerSaveDTO)
        {

            Customer Customer = new Customer();
            if (CustomerSaveDTO != null)
            {
               
                Customer.FirstName = CustomerSaveDTO.FirstName;
                Customer.LastName = CustomerSaveDTO.LastName;
                Customer.Phone = CustomerSaveDTO.Phone;
                Customer.NumberOfOrders = CustomerSaveDTO.NumberOfOrders;
                Customer.Email = CustomerSaveDTO.Email;
                Customer.LastOrderDate = CustomerSaveDTO.LastOrderDate;
                Customer.RestaurantId = CustomerSaveDTO.RestaurantId;
                return Customer;
            }
            return null;
        }
        public async Task<List<CustomerDTO>> GetAllCustomersAsync()
        {
            var Customers = await _CustomerDataAccess.GetAllCustomersAsync();
            if (Customers==null || !Customers.Any())
            {
                throw new BusinessException("empty List");
            }
           
            return Customers.Select(c => new CustomerDTO(

                c.Id,
                c.FirstName,
                c.LastName,
                c.Phone,
                c.NumberOfOrders,
                c.Email,
                c.LastOrderDate,
                c.RestaurantId

            )).ToList(); 
        }
        public async Task<int> AddAsync(CustomerSaveDTO CustomerDTO)
        {
            Customer Customer = ConvertToEntity(CustomerDTO);
            return await _CustomerDataAccess.AddAsync(Customer); 
        }
        public async Task<CustomerDTO> FindAsync(int id)
        {
            Customer customer = await _CustomerDataAccess.FindAsync(id);
            if (customer != null)
            {
                var customerDTO = new CustomerDTO(customer.Id, customer.FirstName, customer.LastName, customer.Phone,
                                              customer.NumberOfOrders, customer.Email, customer.LastOrderDate, customer.RestaurantId);
                return customerDTO;

            }
            return null;
           
        }
        public async Task<CustomerSaveDTO> FindSaveAsync(int id)
        {
            Customer customer = await _CustomerDataAccess.FindAsync(id,true);
            if (customer!= null)
            {
                var customerSaveDTO = new CustomerSaveDTO(customer.FirstName, customer.LastName, customer.Phone, customer.NumberOfOrders,
                                                           customer.Email, customer.LastOrderDate, customer.RestaurantId);
                return customerSaveDTO;

            }
            return null;

        }
        public async Task<bool> UpdateAsync(int id,CustomerSaveDTO CustomerDTO)
        {
            if (await IsExistAsync(id))
            {
                var customer = new Customer
                {
                    Id = id,
                    FirstName = CustomerDTO.FirstName,
                    LastName = CustomerDTO.LastName,
                    Phone = CustomerDTO.Phone,
                    NumberOfOrders = CustomerDTO.NumberOfOrders,
                    Email = CustomerDTO.Email,
                    LastOrderDate = CustomerDTO.LastOrderDate,
                    RestaurantId = CustomerDTO.RestaurantId
                };
               
                return await _CustomerDataAccess.UpdateAsync(customer);
            }
            return false;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _CustomerDataAccess.DeleteAsync(id);
        }
        public async Task<bool> IsExistAsync(int id)
        {
            return await _CustomerDataAccess.ExistsAsync(id);
        }

      
    }
}
