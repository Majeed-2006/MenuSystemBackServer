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
    public class CategoriesConfiguration : IEntityTypeConfiguration<CategoryDTO>
    {
        public void Configure(EntityTypeBuilder<CategoryDTO> entity)
        {
            entity.ToTable("Categories");

            entity.HasKey(p=>p.Id);

            entity.Property(p=>p.Id)
                  .HasColumnName("CtegoryID");

            entity.Property(p => p.Name)
                  .HasColumnName("Name");
            
            entity.Property(p => p.Discription)
                  .HasColumnName("Discription");
            
            entity.Property(p => p.IsAvilable)
                  .HasColumnName("IsAvilable");

            entity.Property(p => p.PrepDuration)
                  .HasColumnName("PrepDuration");
        }
        
    }
}
