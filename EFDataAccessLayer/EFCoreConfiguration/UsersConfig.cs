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
    internal class UsersConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("Users");      // Table name

            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                  .HasColumnName("UserID");

            entity.Property(p => p.FirstName)
                  .HasColumnName("FirstName")
                  .HasMaxLength(30);

            entity.Property(p => p.LastName)
                  .HasColumnName("LastName")
                  .HasMaxLength(30);

            entity.Property(p => p.Address)
                  .HasColumnName("Address")
                  .HasMaxLength(30);

            entity.Property(p => p.UserName)
                  .HasColumnName("UserName")
                  .HasMaxLength(30);

            entity.Property(p => p.Email)
                  .HasColumnName("Email")
                  .HasMaxLength(50);

            entity.Property(p => p.Password)
                  .HasColumnName("Password")
                  .HasMaxLength(64);

            entity.Property(p => p.Phone)
                  .HasColumnName("Phone")
                  .HasMaxLength(30);

            entity.Property(p => p.RoleId)
                  .HasColumnName("RoleID");

            entity.HasOne(p => p.Restaurant)
                  .WithMany(p => p.Users)
                  .HasForeignKey(p => p.RestaurantId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_Users_Restaurant");

            entity.HasOne(p => p.Manager)
                 .WithMany(p => p.Users)
                 .HasForeignKey(p => p.ManagerId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .HasConstraintName("FK_Users_Users");

        }
    }
}
