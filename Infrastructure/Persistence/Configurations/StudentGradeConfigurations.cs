using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public sealed class StudentGradeConfigurations : IEntityTypeConfiguration<StudentGrade>
    {
        public void Configure(EntityTypeBuilder<StudentGrade> builder)
        {
            builder.HasOne(sg => sg.TenantMember)
                   .WithMany(tm => tm.StudentGrades)
                   .HasForeignKey(sg => sg.GraderId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sg => sg.Student)
                   .WithMany(s => s.StudentGrades)
                   .HasForeignKey(sg => sg.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sg => sg.ModuleItem)
                   .WithMany(mi => mi.StudentGrades)
                   .HasForeignKey(sg => sg.TypeId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sg => sg.Tenant)
                   .WithMany(t => t.StudentGrades)
                   .HasForeignKey(sg => sg.TenantId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}