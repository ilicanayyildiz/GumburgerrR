using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Gumburger.Domain.Entities.Concrete;

namespace Gumburger.Infrastructure.EntityTypeConfigurations
{
    public class AddressTypeConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(a => a.FullAddress)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
