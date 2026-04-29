using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class FriendConfigurations : IEntityTypeConfiguration<Friend>
    {
        public void Configure(EntityTypeBuilder<Friend> builder)
        {
            builder.HasIndex(f => new { f.Student1Id, f.Student2Id })
                   .IsUnique();

            builder.HasOne(f => f.Student1)
                   .WithMany(s => s.Students1)
                   .HasForeignKey(f => f.Student1Id)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.Student2)
                   .WithMany(s => s.Students2)
                   .HasForeignKey(f => f.Student2Id)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.ActionStudent)
                   .WithMany(s => s.Actions)
                   .HasForeignKey(f => f.ActionStudentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}