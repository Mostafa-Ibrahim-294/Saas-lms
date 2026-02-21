using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class SessionParticipantConfigurations : IEntityTypeConfiguration<SessionParticipant>
    {
        public void Configure(EntityTypeBuilder<SessionParticipant> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ZoomParticipantId)
                .HasMaxLength(100);

            builder.Property(x => x.ZoomParticipantUuid)
                .HasMaxLength(100);

            builder.Property(x => x.ParticipantEmail)
                .HasMaxLength(255);

            builder.Property(x => x.ParticipantName)
                .HasMaxLength(255);

            builder.Property(x => x.DeviceType)
                .HasMaxLength(50);

            builder.Property(x => x.Notes)
                .HasMaxLength(1000);

            builder.Property(x => x.MarkedBy)
                .HasMaxLength(100);

            builder.Property(x => x.AttendancePercentage)
                .HasPrecision(5, 2);

            builder.Property(x => x.Source)
                .HasConversion<string>()
                .HasMaxLength(30);

            builder.HasIndex(x => x.LiveSessionId);
            builder.HasIndex(x => x.StudentId);
            builder.HasIndex(x => x.Attended);

            builder.HasOne(x => x.LiveSession)
                .WithMany(x => x.Participants)
                .HasForeignKey(x => x.LiveSessionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Student)
                .WithMany(x => x.SessionParticipants)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}