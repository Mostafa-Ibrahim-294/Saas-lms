using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class AssignmentConfigurations : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.HasKey(x => x.ModuleItemId);

            builder.Property(l => l.Attachments)
                 .HasColumnType("jsonb")
                 .HasConversion(
                      v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                      v => JsonSerializer.Deserialize<List<Attachment>>(v, new JsonSerializerOptions
                      {
                          PropertyNameCaseInsensitive = true
                      })!
                 );

            builder.HasOne(a => a.ModuleItem)
                .WithOne(mi => mi.Assignment)
                .HasForeignKey<Assignment>(a => a.ModuleItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
