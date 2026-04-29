using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class StudentSubscriptionConfiguration : IEntityTypeConfiguration<StudentSubscription>
    {
        public void Configure(EntityTypeBuilder<StudentSubscription> builder)
        {
            builder.HasOne(sc => sc.Tenant)
            .WithMany(t => t.StudentSubscriptions)
            .HasForeignKey(sc => sc.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sc => sc.Course)
                .WithMany(c => c.StudentSubscriptions)
                .HasForeignKey(sc => sc.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sc => sc.Student)
                .WithMany(s => s.StudentSubscriptions)
                .HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}