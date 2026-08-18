using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDataAccessLayer.DTOClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFDataAccessLayer.EFCore_Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<CustomerDTO>
    {
        public void Configure(EntityTypeBuilder<CustomerDTO> entity)
        {
            entity.ToTable("Customers");      // Table name

            entity.HasKey(p => p.Id);


            entity.Property(p => p.Id)
                  .HasColumnName("CustomerID"); // Primary Key

            entity.Property(p => p.FirstName)
                  .HasColumnName("FirstName");

            entity.Property(p => p.LastName)
                  .HasColumnName("LastName");

            entity.Property(p => p.Phone)
                  .HasColumnName("Phone");

            entity.Property(p => p.NumberOfOrders)
                  .HasColumnName("NumberOfOrders");

            entity.Property(p => p.Email)
                  .HasColumnName("Email");

            entity.Property(p => p.LastOrderDate)
                  .HasColumnName("LastOrderDate");
        }
    }
}
