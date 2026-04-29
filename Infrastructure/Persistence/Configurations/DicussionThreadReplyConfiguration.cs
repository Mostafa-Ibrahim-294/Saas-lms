using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public sealed class DicussionThreadReplyConfiguration : IEntityTypeConfiguration<DicussionThreadReply>
    {
        public void Configure(EntityTypeBuilder<DicussionThreadReply> builder)
        {
            builder.Property(dtr => dtr.Body)
                .IsRequired()
                .HasMaxLength(3000);

            builder.HasOne(dtr => dtr.User)
                .WithMany(u => u.DicussionReplies)
                .HasForeignKey(dtr => dtr.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(dtr => dtr.DicussionThread)
                .WithMany(dt => dt.Replies)
                .HasForeignKey(dtr => dtr.DicussionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(dtr => dtr.Tenant)
                .WithMany(t => t.DicussionReplies)
                .HasForeignKey(dtr => dtr.TenantId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}