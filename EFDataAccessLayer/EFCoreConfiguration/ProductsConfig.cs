using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDataAccessLayer.EntityClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFDataAccessLayer.EFCore_Configuration
{
    internal class ProductsConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.ToTable("Products");      // Table name

            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                  .HasColumnName("ProductID");

            entity.Property(p => p.IsAvailable)
                  .HasColumnName("IsAvilable");

            entity.Property(p => p.Name)
                  .HasColumnName("Name")
                   .HasMaxLength(40);
            entity.Property(p => p.CreatedByUserId)
                  .HasColumnName("CreatedByUserID");
            
            entity.Property(p => p.CategoryId)
                  .HasColumnName("CategoryID");

            entity.Property(p => p.Description)
                  .HasColumnName("Description")
                  .HasMaxLength(100);

            entity.Property(p => p.Price)
                  .HasColumnName("Price");

            entity.Property(p => p.RestaurantId)
                  .HasColumnName("RestuarantId");

            entity.HasOne(p => p.CreatedByUser)
                  .WithMany(p => p.Products)
                  .HasForeignKey(p => p.CreatedByUserId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_Products_Users");

            entity.HasOne(p => p.Restaurant)
                 .WithMany(p => p.Products)
                 .HasForeignKey(p => p.RestaurantId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .HasConstraintName("FK_Products_Restaurants");

            entity.HasOne(p => p.Category)
                 .WithMany(p => p.Products)
                 .HasForeignKey(p => p.CategoryId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .HasConstraintName("FK_Products_Categories");



        }
    }
}
