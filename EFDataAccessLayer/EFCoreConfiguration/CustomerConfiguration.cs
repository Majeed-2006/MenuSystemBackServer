using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDataAccessLayer.EntityClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EFDataAccessLayer.EntityClasses;

namespace EFDataAccessLayer.EFCore_Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> entity)
        {
            entity.ToTable("Customers");      // Table name

            entity.HasKey(p => p.Id);


            entity.Property(p => p.Id)
                  .HasColumnName("CustomerID"); // Primary Key

            entity.Property(p => p.FirstName)
                  .HasColumnName("FirstName")
                  .HasMaxLength(40);

            entity.Property(p => p.LastName)
                  .HasColumnName("LastName")
                  .HasMaxLength(40); 

            entity.Property(p => p.Phone)
                  .HasColumnName("Phone")
                  .HasMaxLength(40);

            entity.Property(p => p.NumberOfOrders)
                  .HasColumnName("NumberOfOrders");

            entity.Property(p => p.Email)
                  .HasColumnName("Email")
                  .HasMaxLength(40);

            entity.Property(p => p.RestaurantId)
                .HasColumnName("RestaurantID");

            entity.Property(p => p.LastOrderDate)
                  .HasColumnName("LastOrderDate")
                  .HasColumnType("datetime")
                  .HasDefaultValueSql("GETDATE()");

            entity.HasOne(p => p.Restaurant)
              .WithMany(p => p.Customers)
              .HasForeignKey(p => p.RestaurantId)
              .OnDelete(DeleteBehavior.NoAction)
              .HasConstraintName("FK_Customers_Restaurants");
        }
    }
}
