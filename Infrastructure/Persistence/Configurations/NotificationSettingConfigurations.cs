using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class NotificationSettingConfigurations : IEntityTypeConfiguration<NotificationSetting>
    {
        public void Configure(EntityTypeBuilder<NotificationSetting> builder)
        {
            builder.HasOne(x => x.Tenant)
                .WithOne(t => t.NotificationSetting)
                .HasForeignKey<NotificationSetting>(x => x.TenantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
