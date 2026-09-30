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
    internal class RolesConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> entity)
        {
            entity.ToTable("Roles");      // Table name

            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id)
               .HasColumnName("RoleID");


            entity.Property(p => p.Name)
                .HasColumnName("Name")
                .HasMaxLength(30);

        }
    }
}