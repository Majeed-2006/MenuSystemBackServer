using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFDataAccessLayer.EntityClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFDataAccessLayer.EFCore_Configuration
{
    public class OrdersConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> entity)
        {
            entity.ToTable("Orders");

            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                .HasColumnName("OrderID");

            entity.Property(p => p.WaiterId)
                .HasColumnName("WaiterID");

            entity.Property(p => p.OrderType)
                .HasColumnName("OrderType");

            entity.Property(p => p.PaymentStatus)
                .HasColumnName("PaymentStatus");

            entity.Property(p => p.CashierId)
                .HasColumnName("CashierID");

            entity.Property(p => p.CustomerId)
                .HasColumnName("CustomerID");

            entity.Property(p => p.DriverId)
                .HasColumnName("DriverID");
                
            entity.Property(p  => p.OrderDateTime)
                .HasColumnName("OderDateTime")
                  .HasDefaultValueSql("GETDATE()");

            entity.Property(p => p.OrderStatusId)
                .HasColumnName("OrderStatusID");

            entity.Property(p => p.RestaurantId)
                .HasColumnName("RestaurantID");


            entity.HasOne(p => p.Restaurant)
            .WithMany(p => p.Orders)
            .HasForeignKey(p => p.RestaurantId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Orders_Restaurants");

            entity.HasOne(p => p.Customer)
            .WithMany(p => p.Orders)
            .HasForeignKey(p => p.CustomerId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Orders_Customers");

            entity.HasOne(p => p.OrderStatus)
           .WithMany(p => p.Orders)
           .HasForeignKey(p => p.OrderStatusId)
           .OnDelete(DeleteBehavior.Restrict)
           .HasConstraintName("FK_Orders_OrderStatuses");

            entity.HasOne(p => p.Cashier)
            .WithMany(p => p.CashierOrders)
            .HasForeignKey(p => p.CashierId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Orders_Cashier");

            entity.HasOne(p => p.Waiter)
           .WithMany(p => p.WaiterOrders)
           .HasForeignKey(p => p.WaiterId)
           .OnDelete(DeleteBehavior.Restrict)
           .HasConstraintName("FK_Orders_Waiter");

            entity.HasOne(p => p.Driver)
           .WithMany(p => p.DriverOrders)
           .HasForeignKey(p => p.DriverId)
           .OnDelete(DeleteBehavior.Restrict)
           .HasConstraintName("FK_Orders_Driver");
        }
    }
}
