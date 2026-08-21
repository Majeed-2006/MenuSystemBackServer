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
    internal class UsersConfig : IEntityTypeConfiguration<UserDTO>
    {
        public void Configure(EntityTypeBuilder<UserDTO> entity)
        {
            entity.ToTable("Users");      // Table name

            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                  .HasColumnName("UserID");

            entity.Property(p => p.FirstName)
                  .HasColumnName("FirstName");

            entity.Property(p => p.LastName)
                  .HasColumnName("LastName");

            entity.Property(p => p.Address)
                  .HasColumnName("Address");

            entity.Property(p => p.UseName)
                  .HasColumnName("UserName");

            entity.Property(p => p.Email)
                  .HasColumnName("Email");

            entity.Property(p => p.Password)
                  .HasColumnName("Password");

            entity.Property(p => p.Phone)
                  .HasColumnName("Phone");

            entity.Property(p => p.Role)
                  .HasColumnName("Role");

        }
    }
}
