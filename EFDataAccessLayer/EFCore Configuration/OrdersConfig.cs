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
    public class OrdersConfig : IEntityTypeConfiguration<OrderDTO>
    {
        public void Configure(EntityTypeBuilder<OrderDTO> entity)
        {
            entity.ToTable("Orders");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .HasColumnName("OrderID");

            entity.Property(x => x.WaiterID)
                .HasColumnName("WaiterID");

            entity.Property(x => x.OrderrType)
                .HasColumnName("OrderrType");

            entity.Property(x => x.PaymentStatus)
                .HasColumnName("PaymentStatus");

            entity.Property(x => x.CashierID)
                .HasColumnName("CashierID");

            entity.Property(x => x.CustomerID)
                .HasColumnName("CustomerID");

            entity.Property(x => x.DriverID)
                .HasColumnName("DriverID");

            entity.Property(x => x.OrderDateTime)
                .HasColumnName("OrderDateTime");

            entity.Property(x => x.OrderrType)
                .HasColumnName("OrderrType");

            entity.Property(x => x.OrderStatusID)
                .HasColumnName("OrderStatusID");
        }
    }
}
