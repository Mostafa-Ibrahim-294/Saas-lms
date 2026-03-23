using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class AssignmentSubmissionConfigurations : IEntityTypeConfiguration<AssignmentSubmission>
    {
        public void Configure(EntityTypeBuilder<AssignmentSubmission> builder)
        {
            builder.HasOne(s => s.Assignment)
                .WithMany(a => a.Submissions)
                .HasForeignKey(s => s.AssignmentId);
            builder.HasOne(s => s.Student)
                .WithOne(s => s.AssignmentSubmission)
                .HasForeignKey<AssignmentSubmission>(s => s.StudentId);

        }
    }
}
