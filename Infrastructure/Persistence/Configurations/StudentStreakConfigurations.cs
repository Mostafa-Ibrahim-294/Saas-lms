using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class StudentStreakConfigurations : IEntityTypeConfiguration<StudentStreak>
    {
        public void Configure(EntityTypeBuilder<StudentStreak> builder)
        {
            builder.HasOne(ss => ss.Student)
                   .WithOne(s => s.StudentStreak)
                   .HasForeignKey<StudentStreak>(ss => ss.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}