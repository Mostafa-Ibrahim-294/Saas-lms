using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class LevelConfigurations : IEntityTypeConfiguration<Level>
    {
        public void Configure(EntityTypeBuilder<Level> builder)
        {
            builder.Property(l => l.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.HasIndex(l => l.LevelNumber)
                   .IsUnique();
        }
    }
}