using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    internal class PermissionConfigurations : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.Property(p => p.Name)
                   .HasMaxLength(200)
                   .IsRequired();
            builder.Property(p => p.Module)
                   .HasMaxLength(100)
                   .IsRequired();
            builder.Property(p => p.Description)
                   .HasMaxLength(500);
        }
    }
}
