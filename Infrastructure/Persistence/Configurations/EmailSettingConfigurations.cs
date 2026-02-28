using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class EmailSettingConfigurations : IEntityTypeConfiguration<EmailSetting>
    {
        public void Configure(EntityTypeBuilder<EmailSetting> builder)
        {
            builder.HasOne(x => x.Tenant)
                .WithOne(t => t.EmailSetting)
                .HasForeignKey<EmailSetting>(x => x.TenantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
