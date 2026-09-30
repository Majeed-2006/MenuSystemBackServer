
using EFDataAccessLayer.EntityClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLayer.SettingClasses
{
    public class AppDbContext : DbContext
    {
        //add a refrence to the classes we want this is optional but very clean 
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!; 
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<OrderStatus> OrderStatuses { get; set; } = null!;
        public DbSet<Restaurant> Restaurants { get; set; } = null!;
        public DbSet<Category> Categories { get; set; }=null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Role>Roles { get; set; } = null!;
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        // takes into account all the entity Framecore configuration and builds them here 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

       
    }

}
