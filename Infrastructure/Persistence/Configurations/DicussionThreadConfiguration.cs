using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public sealed class DicussionThreadConfiguration : IEntityTypeConfiguration<DicussionThread>
    {
        public void Configure(EntityTypeBuilder<DicussionThread> builder)
        {
            builder.Property(dt => dt.Content)
                .IsRequired()
                .HasMaxLength(5000);

            builder.HasOne(dt => dt.User)
                .WithMany(u => u.DicussionThreads)
                .HasForeignKey(dt => dt.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(dt => dt.ModuleItem)
                .WithMany(mi => mi.DicussionThreads)
                .HasForeignKey(dt => dt.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(dt => dt.Course)
                .WithMany(c => c.DicussionThreads)
                .HasForeignKey(dt => dt.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(dt => dt.Module)
                .WithMany(m => m.DicussionThreads)
                .HasForeignKey(dt => dt.ModuleId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(dt => dt.Tenant)
                .WithMany(t => t.DicussionThreads)
                .HasForeignKey(dt => dt.TenantId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}