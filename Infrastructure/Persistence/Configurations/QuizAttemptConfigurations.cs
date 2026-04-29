using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class QuizAttemptConfigurations : IEntityTypeConfiguration<QuizAttempt>
    {
        public void Configure(EntityTypeBuilder<QuizAttempt> builder)
        {
            builder.HasOne(qa => qa.Quiz)
                    .WithMany(q => q.Attempts)
                    .HasForeignKey(qa => qa.ModuleItemId)
                    .OnDelete(DeleteBehavior.Cascade);
    
                builder.HasOne(qa => qa.Student)
                    .WithOne(s => s.QuizAttempt)
                    .HasForeignKey<QuizAttempt>(qa => qa.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
