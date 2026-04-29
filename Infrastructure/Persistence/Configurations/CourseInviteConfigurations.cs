using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public sealed class CourseInviteConfigurations : IEntityTypeConfiguration<CourseInvite>
    {
        public void Configure(EntityTypeBuilder<CourseInvite> builder)
        {
            builder.HasIndex(ti => ti.Token).IsUnique();

            builder.Property(ti => ti.Token)
                   .HasMaxLength(64)
                   .IsRequired();

            builder.Property(ti => ti.Email)
                   .HasMaxLength(256)
                   .IsRequired();

            builder.Property(ti => ti.Status)
                   .IsRequired();

            builder.Property(ti => ti.CreatedAt)
                   .IsRequired();

            builder.Property(ti => ti.ExpiresAt)
                   .IsRequired();

            builder.HasOne(ti => ti.TenantMember)
                   .WithMany(tm => tm.CourseInvites)
                   .HasForeignKey(ti => ti.InvitedBy)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ci => ci.Course)
                   .WithMany(c => c.CourseInvites)
                   .HasForeignKey(ci => ci.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
