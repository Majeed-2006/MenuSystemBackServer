
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
        private readonly AppDbContext _Context;

        public CustomerDataAccess(AppDbContext context)
        {
            _Context = context;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            try
            {
                return await _Context.Customers.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
          
        }

        public async Task<Customer?> FindAsync(int id,bool isTracking =false)
        {
            if(!isTracking)
               return await _Context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            else
                return await _Context.Customers.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<bool> UpdateAsync(Customer Customer)
        {
            Customer  UpdatedCustomer = await  _Context.Customers.FirstOrDefaultAsync(x=> x.Id ==Customer.Id);

            if (UpdatedCustomer!= null)
            {
                UpdatedCustomer.FirstName = Customer.FirstName;
                UpdatedCustomer.LastName = Customer.LastName;
                UpdatedCustomer.Email = Customer.Email;
                UpdatedCustomer.Phone = Customer.Phone;
                UpdatedCustomer.LastOrderDate = Customer.LastOrderDate;
                UpdatedCustomer.NumberOfOrders = Customer.NumberOfOrders;
                await _Context.SaveChangesAsync();
                return true;
            }
            return false;
          }


        //public async Task<bool> ChangePasswordAsync(int id, string newHashPassword)
        //{
        //    var user = new User
        //    {
        //        Id = id
        //    };
        //    _Context.Attach(user);
        //    user.Password = newHashPassword;
        //    int rowsAffected = await _Context.SaveChangesAsync();
        //    return rowsAffected > 0;

        //}

        
        public async Task<bool> UpdateFirstNameAsync(int id ,string firstName)
        {
           
            var customer = new Customer
            {
                Id = id
            };
           
            _Context.Attach(customer);
            customer.FirstName = firstName;
            int rowsAffected = await _Context.SaveChangesAsync();
            return rowsAffected > 0;
        }
        public async Task<bool> UpdateLastNameAsync(int id, string lastName)
        {

            var customer = new Customer
            {
                Id = id
            };

            _Context.Attach(customer);
            customer.FirstName = lastName;
            int rowsAffected = await _Context.SaveChangesAsync();
            return rowsAffected > 0;
        }
        public async Task<bool> UpdatePhoneAsync(int id, string phone)
        {

            var customer = new Customer
            {
                Id = id
            };

            _Context.Attach(customer);
            customer.Phone = phone;
            int rowsAffected = await _Context.SaveChangesAsync();
            return rowsAffected > 0;
        }
        public async Task<bool> UpdateNumberOfOrdersNameAsync(int id, int numberOfOders)
        {

            var customer = new Customer
            {
                Id = id
            };

            _Context.Attach(customer);
            customer.NumberOfOrders = numberOfOders;
            int rowsAffected = await _Context.SaveChangesAsync();
            return rowsAffected > 0;
        }
        public async Task<bool> UpdateEmailAsync(int id, string email)
        {

            var customer = new Customer
            {
                Id = id
            };

            _Context.Attach(customer);
            customer.Email = email;
            int rowsAffected = await _Context.SaveChangesAsync();
            return rowsAffected > 0;
        }
        public async Task<bool> UpdateLastOrderDateAsync(int id, DateTime lastOrderDate)
        {

            var customer = new Customer
            {
                Id = id
            };

            _Context.Attach(customer);
            customer.LastOrderDate = lastOrderDate;
            int rowsAffected = await _Context.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public async Task<int> AddAsync(Customer newCustomer)
        {
            try
            {
                _Context.Customers.Add(newCustomer);
                await _Context.SaveChangesAsync();
                return newCustomer.Id;
            }
            catch (Exception ex)
            {
                return -1;
            }
           
           
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                Customer Customer = await _Context.Customers.FirstOrDefaultAsync(x => x.Id == id);
                if (Customer == null)
                {
                    return false;
                }
                _Context.Customers.Remove(Customer);
                await _Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public async Task<bool> ExistsAsync(int id)
        {
           
            return await _Context.Customers.AnyAsync(c => c.Id == id);
        }
    }
}