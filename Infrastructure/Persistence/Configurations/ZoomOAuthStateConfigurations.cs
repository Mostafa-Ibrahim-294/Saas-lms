using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class ZoomOAuthStateConfigurations : IEntityTypeConfiguration<ZoomOAuthState>
    {
        public void Configure(EntityTypeBuilder<ZoomOAuthState> builder)
        {
            builder.Property(x => x.StateToken)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(x => x.StateToken).IsUnique();

            builder.HasOne(x => x.Tenant)
                .WithMany(x => x.ZoomOAuthStates)
                .HasForeignKey(x => x.TenantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User)
                .WithMany(x => x.ZoomOAuthStates)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}