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
    public class CategoriesConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entity)
        {
            entity.ToTable("Categories");

            entity.HasKey(p=>p.Id);

            entity.Property(p=>p.Id)
                  .HasColumnName("CtegoryID");

            entity.Property(p => p.Name)
                  .HasColumnName("Name")
                  .HasMaxLength(50);
            
            entity.Property(p => p.Description)
                  .HasColumnName("Description")
                  .HasMaxLength(200);
            
            entity.Property(p => p.IsAvailable)
                  .HasColumnName("IsAvailable");

            entity.Property(p => p.PrepDuration)
                  .HasColumnName("PrepDuration");

            entity.Property(p => p.RestaurantId)
                  .HasColumnName("RestuarantId");

            entity.HasOne(p => p.Restaurant)
                .WithMany(p => p.Categories)
                .HasForeignKey(p => p.RestaurantId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_Categories_Restaurants");

            entity.HasOne(p => p.CreatedByUser)
               .WithMany(p => p.Categories)
               .HasForeignKey(p => p.CreatedByUserId)
               .OnDelete(DeleteBehavior.Restrict)
               .HasConstraintName("FK_Categories_Users");
        }

    }
}
