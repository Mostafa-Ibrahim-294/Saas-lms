using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public sealed class DicussionThreadReadConfiguration : IEntityTypeConfiguration<DicussionThreadRead>
    {
        public void Configure(EntityTypeBuilder<DicussionThreadRead> builder)
        {
            builder.HasIndex(dr => new { dr.DicussionId, dr.UserId })
                .IsUnique();

            builder.HasOne(dtr => dtr.User)
                .WithMany(u => u.DicussionReads)
                .HasForeignKey(dtr => dtr.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(dtr => dtr.DicussionThread)
                .WithMany(dr => dr.DicussionReads)
                .HasForeignKey(dtr => dtr.DicussionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(dtr => dtr.Tenant)
                .WithMany(t => t.DicussionReads)
                .HasForeignKey(dtr => dtr.TenantId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}