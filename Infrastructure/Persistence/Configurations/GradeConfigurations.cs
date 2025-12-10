using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    internal class GradeConfigurations : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.Property(g => g.Value)
                   .HasMaxLength(100)
                   .IsRequired();
            builder.Property(g => g.Label)
                     .HasMaxLength(100)
                     .IsRequired();
        }
    }
}
