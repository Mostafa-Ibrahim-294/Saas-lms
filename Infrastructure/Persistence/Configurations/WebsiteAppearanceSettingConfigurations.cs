using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class WebsiteAppearanceSettingConfigurations : IEntityTypeConfiguration<WebsiteAppearanceSetting>
    {
        public void Configure(EntityTypeBuilder<WebsiteAppearanceSetting> builder)
        {
            builder.Property(x => x.FavIcon)
                .HasMaxLength(2048);

            builder.HasOne(x => x.Tenant)
                .WithOne(t => t.WebsiteAppearnceSetting)
                .HasForeignKey<WebsiteAppearanceSetting>(x => x.TenantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
