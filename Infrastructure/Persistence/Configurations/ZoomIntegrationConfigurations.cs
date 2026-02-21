using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class ZoomIntegrationConfigurations : IEntityTypeConfiguration<ZoomIntegration>
    {
        public void Configure(EntityTypeBuilder<ZoomIntegration> builder)
        {
            builder.Property(x => x.ZoomUserId)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.ZoomAccountId)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.ZoomEmail)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.ZoomDisplayName)
                .HasMaxLength(255);

            builder.Property(x => x.ZoomAccountType)
                .HasMaxLength(50);

            builder.Property(x => x.AccessToken)
                .IsRequired();

            builder.Property(x => x.RefreshToken)
                .IsRequired();

            builder.HasIndex(x => new { x.UserId, x.TenantId }).IsUnique();

            builder.HasOne(x => x.Tenant)
                .WithMany(x => x.ZoomIntegrations)
                .HasForeignKey(x => x.TenantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User)
                .WithMany(x => x.ZoomIntegrations)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}