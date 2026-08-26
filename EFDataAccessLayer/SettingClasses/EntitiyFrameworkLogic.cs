using EFDataAccessLayer.DTOClasses;
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
    internal class EntitiyFrameworkLogic : DbContext
    {
        //add a refrence to the classes we want this is optional but very clean 
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<OrderItem> OrderItem { get; set; } = null!; 
        
        // takes into account all the entity Framecore configuration and builds them here 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EntitiyFrameworkLogic).Assembly);
        }

        //main confugration ,to configure or change go to connstr.json at debug/bin/net8.0
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var configurations = new ConfigurationBuilder()
                .AddJsonFile("connstr.json")
                .Build();
            var constr = configurations.GetSection("Const").Value;
            optionsBuilder.UseSqlServer(constr);
        }
    }

}
