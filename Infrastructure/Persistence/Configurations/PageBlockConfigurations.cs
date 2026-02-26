using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class PageBlockConfigurations : IEntityTypeConfiguration<PageBlock>
    {
        public void Configure(EntityTypeBuilder<PageBlock> builder)
        {
            builder.Property(x => x.Order)
                   .IsRequired();

            builder.Property(x => x.Content)
                   .IsRequired();

            builder.Property(x => x.Visible)
                   .IsRequired();

            builder.Property(x => x.Props)
                   .HasColumnType("jsonb")
                   .IsRequired();

            builder.HasOne(x => x.TenantPage)
                   .WithMany(x => x.PageBlocks)
                   .HasForeignKey(x => x.TenantPageId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.BlockType)
                   .WithMany(x => x.PageBlocks)
                   .HasForeignKey(x => x.BlockTypeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
