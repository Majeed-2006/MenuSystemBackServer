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
    public class OrderItemsConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> entity)
        {
            entity.ToTable("OrderItems");

            entity.HasKey(p => p.Id);

            entity.Property(p=> p.Id)
                .HasColumnName("OrderItemID");

            entity.Property(p => p.ProductId)
                .HasColumnName("ProductID");

            entity.Property(p => p.Quantity)
                .HasColumnName("Quantity");

            entity.Property(p => p.UnitPrice)
                .HasColumnName("UnitPrice");

            entity.Property(p => p.OrderId)
                .HasColumnName("OrderID");

            entity.HasOne(p => p.Order)
              .WithMany(p => p.OrderItems)
              .HasForeignKey(p => p.OrderId)
              .OnDelete(DeleteBehavior.Restrict)
              .HasConstraintName("FK_OrderItems_Orders");

            entity.HasOne(p => p.Product)
             .WithMany(p => p.OrdersItems)
             .HasForeignKey(p => p.ProductId)
             .OnDelete(DeleteBehavior.Restrict)
             .HasConstraintName("FK_OrderItems_Products");
        }
    }
}
