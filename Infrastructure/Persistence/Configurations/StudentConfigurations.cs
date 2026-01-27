using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class StudentConfigurations : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(s => s.ParentName)
                .HasMaxLength(100);
            builder.Property(s => s.ParentEmail)
                .HasMaxLength(100);
            builder.Property(s => s.ParentPhone)
                .HasMaxLength(15);
        }
    }
}
