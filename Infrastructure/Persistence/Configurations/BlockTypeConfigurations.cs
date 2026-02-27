using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class BlockTypeConfigurations : IEntityTypeConfiguration<BlockType>
    {
        public void Configure(EntityTypeBuilder<BlockType> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(x => x.DisplayName)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Description)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(x => x.Icon)
                   .IsRequired();

            builder.Property(x => x.Schema)
                   .HasColumnType("jsonb")
                   .IsRequired();
        }
    }
}
