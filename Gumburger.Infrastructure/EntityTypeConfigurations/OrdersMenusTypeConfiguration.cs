using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gumburger.Domain.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Gumburger.Infrastructure.EntityTypeConfigurations
{
    public class OrdersMenusTypeConfiguration : IEntityTypeConfiguration<OrdersMenus>
    {
        public void Configure(EntityTypeBuilder<OrdersMenus> builder)
        {
            builder.HasKey(om => new { om.OrderId, om.MenuId });
            builder.HasOne(om => om.Order).WithMany(o => o.OrdersMenus).HasForeignKey(om => om.OrderId);
            builder.HasOne(om => om.Menu).WithMany(m => m.OrdersMenus).HasForeignKey(om => om.MenuId);

            builder.Property(om => om.MenuPrice)
                .IsRequired()
                .HasColumnType("decimal(4,2)")
                .HasPrecision(4, 2);

            builder.Property(om => om.MenuTotalPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasPrecision(18, 2);
        }
    }
}
