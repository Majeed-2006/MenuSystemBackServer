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
    internal class ProductsConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.ToTable("Products");      // Table name

            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                  .HasColumnName("ProductID");

            entity.Property(p => p.IsAvilable)
                  .HasColumnName("IsAvilable");

            entity.Property(p => p.Name)
                  .HasColumnName("Name");

            entity.Property(p => p.CreatedByUserID)
                  .HasColumnName("CreatedByUserID");
            
            entity.Property(p => p.CategoryID)
                  .HasColumnName("CategoryID");

            entity.Property(p => p.Description)
                  .HasColumnName("Description");

            entity.Property(p => p.Price)
                  .HasColumnName("Price");
        }
    }
}
