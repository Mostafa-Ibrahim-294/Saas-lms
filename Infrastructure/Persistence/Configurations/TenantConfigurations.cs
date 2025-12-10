using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class TenantConfigurations : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.Property(t => t.Logo)
                   .HasMaxLength(200);
            builder.Property(t => t.SubDomain)
                   .HasMaxLength(200)
                   .IsRequired();
            builder.HasIndex(t => t.SubDomain)
                   .IsUnique();

        }
    }
}
