using EFDataAccessLayer.EntityClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLayer.EFCoreConfiguration
{
    internal class RestarantConfig : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> entity)
        {
            entity.ToTable("Restaurants");      // Table name

            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id)
               .HasColumnName("RestaurantID");

            entity.Property(p => p.Name)
                  .HasColumnName("Name")
                  .HasMaxLength(100);

            entity.Property(p => p.SubDomain)
                  .HasColumnName("SubDomain")
                  .HasMaxLength(50);

            entity.HasIndex(p => p.SubDomain)
                  .IsUnique();

            entity.Property(p => p.CreatedAt)
                  .HasColumnName("CreatedAt")
                  .HasColumnType("datetime")
                  .HasDefaultValueSql("GetDate()");

            

           

        }
    }
}
