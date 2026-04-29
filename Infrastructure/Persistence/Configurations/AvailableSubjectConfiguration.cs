using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    internal class AvailableSubjectConfiguration : IEntityTypeConfiguration<AvailableSubject>
    {
        public void Configure(EntityTypeBuilder<AvailableSubject> builder)
        {
            builder.Property(x => x.Grade)
                   .HasMaxLength(100);

            builder.Property(x => x.Key)
                   .HasMaxLength(100);

            builder.Property(x => x.DisplayName)
                   .HasMaxLength(100);

            builder.Property(x => x.Semester)
                   .HasMaxLength(100);

            builder.HasIndex(x => x.Key)
                   .IsUnique();
        }
    }
}