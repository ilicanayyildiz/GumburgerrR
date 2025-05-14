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
    public class MenuTypeConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.Property(m => m.MenuName)
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(m => m.MenuPrice)
                .IsRequired()
                .HasColumnType("decimal(4,2)")
                .HasPrecision(6, 2);

            builder.HasAlternateKey(m => new { m.MenuName, m.MenuSize });
        }
    }
}
