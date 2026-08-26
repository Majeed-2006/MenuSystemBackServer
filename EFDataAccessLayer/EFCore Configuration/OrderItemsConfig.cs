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
    public class OrderItemsConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> entity)
        {
            entity.ToTable("OrderItems");

            entity.HasKey(x => x.Id);

            entity.Property(x=> x.Id)
                .HasColumnName("OrderItemID");

            entity.Property(x => x.ProductID)
                .HasColumnName("ProductID");

            entity.Property(x => x.Quantity)
                .HasColumnName("Quantity");

            entity.Property(x => x.UnitPrice)
                .HasColumnName("UnitPrice");

            entity.Property(x => x.OrderID)
                .HasColumnName("OrderID");
        }
    }
}
