using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class StudentConfigurations : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(s => s.ParentEmail)
               .HasMaxLength(100);

            builder.Property(s => s.Grade)
                .HasMaxLength(100);

            builder.Property(s => s.Semester)
                .HasMaxLength(50);

            builder.Property(s => s.Goal)
                .HasMaxLength(250);

            builder.Property(s => s.InviteCode)
                .HasMaxLength(10);

            builder.HasIndex(s => s.InviteCode)
                .IsUnique();
        }
    }
}