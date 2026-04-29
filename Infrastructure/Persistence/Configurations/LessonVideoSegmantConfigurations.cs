using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class LessonVideoSegmantConfigurations : IEntityTypeConfiguration<LessonVideoSegmant>
    {
        public void Configure(EntityTypeBuilder<LessonVideoSegmant> builder)
        {
            builder.HasOne(x => x.LessonView)
                   .WithMany(x => x.VideoSegmants)
                   .HasForeignKey(x => x.LessonViewId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("LessonVideoSegmants", t =>
            {
                t.HasCheckConstraint(
                    "CK_LessonVideoSegmants_Seconds",
                    "\"StartSecond\" >= 0 AND \"EndSecond\" > \"StartSecond\""
                );
            });
        }
    }
}