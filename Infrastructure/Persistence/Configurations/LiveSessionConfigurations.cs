using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class LiveSessionConfigurations : IEntityTypeConfiguration<LiveSession>
    {
        public void Configure(EntityTypeBuilder<LiveSession> builder)
        {
            builder.Property(x => x.Title)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(1000);

            builder.Property(x => x.ZoomMeetingId)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.ZoomHostId)
                .HasMaxLength(100);

            builder.Property(x => x.ZoomJoinUrl)
                .HasMaxLength(500);

            builder.Property(x => x.ZoomStartUrl)
                .HasMaxLength(500);

            builder.Property(x => x.ZoomHostEmail)
                .HasMaxLength(255);

            builder.Property(x => x.ZoomPassword)
                .HasMaxLength(50);

            builder.Property(x => x.RecordingUrl)
                .HasMaxLength(500);

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.HasIndex(x => x.ZoomMeetingId).IsUnique();

            builder.HasOne(x => x.Course)
                .WithMany(x => x.LiveSessions)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Tenant)
                .WithMany(x => x.LiveSessions)
                .HasForeignKey(x => x.TenantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Host)
                .WithMany(x => x.LiveSessions)
                .HasForeignKey(x => x.HostMemberId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ZoomIntegration)
                .WithMany(x => x.LiveSessions)
                .HasForeignKey(x => x.ZoomIntegrationId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}