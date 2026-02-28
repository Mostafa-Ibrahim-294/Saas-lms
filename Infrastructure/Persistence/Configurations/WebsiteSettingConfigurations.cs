using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class WebsiteSettingConfigurations : IEntityTypeConfiguration<WebsiteSetting>
    {
        public void Configure(EntityTypeBuilder<WebsiteSetting> builder)
        {
            builder.HasOne(x => x.Tenant)
                .WithOne(t => t.WebsiteSetting)
                .HasForeignKey<WebsiteSetting>(x => x.TenantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
