using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class ScheduleConfigurations : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.Property(s => s.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(s => s.Description)
                .HasMaxLength(1000);

            builder.HasIndex(s => new { s.TenantId, s.StartAt, s.EndAt })
                .HasDatabaseName("IX_Schedules_TenantId_DateRange");

            builder.HasIndex(s => new { s.CourseId, s.Type })
                .HasDatabaseName("IX_Schedules_CourseId_Type");

            builder.ToTable("Schedules", t =>
            {
                t.HasCheckConstraint(
                    "CK_Schedule_End_After_Start",
                    "\"EndAt\" > \"StartAt\""
                );

                t.HasCheckConstraint(
                    "CK_Schedule_Repeat_Valid",
                    "\"RepeatEvent\" = false OR (\"RepeatFrequency\" IS NOT NULL AND \"RepeatPeriodEnd\" IS NOT NULL)"
                );
            });

            builder.HasOne(s => s.Tenant)
                .WithMany(t => t.Schedules)
                .HasForeignKey(s => s.TenantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Course)
                .WithMany(c => c.Schedules)
                .HasForeignKey(s => s.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}