using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class TenantPageConfigurations : IEntityTypeConfiguration<TenantPage>
    {
        public void Configure(EntityTypeBuilder<TenantPage> builder)
        {
            builder.Property(x => x.Title)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(x => x.MetaTitle)
                   .HasMaxLength(500);

            builder.Property(x => x.MetaDescription)
                   .HasMaxLength(1000);

            builder.Property(x => x.Url)
                   .HasMaxLength(300)
                   .IsRequired();

            builder.Property(x => x.Status)
                   .IsRequired();

            builder.HasIndex(x => new { x.TenantId, x.Url })
                   .IsUnique();

            builder.HasOne(x => x.Tenant)
                   .WithMany(x => x.TenantPages)
                   .HasForeignKey(x => x.TenantId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}