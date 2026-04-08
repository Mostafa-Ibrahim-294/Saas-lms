using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    internal sealed class AnnouncementConfiguration : IEntityTypeConfiguration<Announcement>
    {
        public void Configure(EntityTypeBuilder<Announcement> builder)
        {
            builder.Property(a => a.Title)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(a => a.Content)
                   .IsRequired()
                   .HasMaxLength(2000);

            builder.Property(a => a.TargetCourseIds)
                   .HasColumnType("integer[]");

            builder.HasOne(ann => ann.Tenant)
                .WithMany(t => t.Announcements)
                .HasForeignKey(a => a.TenantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ann => ann.TenantMember)
                .WithMany(tm => tm.Announcements)
                .HasForeignKey(a => a.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}