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
    public class OrderStatusesConfig : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> entity)
        {
            entity.ToTable("OrderStatuses");      // Table name

            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                  .HasColumnName("StateID");
                   
            entity.Property(p => p.EngName)
                  .HasColumnName("SatetNameEng")
                   .HasMaxLength(40);

            entity.Property(p => p.ArName)
                  .HasColumnName("StateNameAr")
                  .HasMaxLength(40);
        }
    }
}
