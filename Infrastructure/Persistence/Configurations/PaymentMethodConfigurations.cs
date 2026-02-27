using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class PaymentMethodConfigurations : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.Property(x => x.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Details)
                .HasColumnType("jsonb")
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(v, (JsonSerializerOptions?)null)!
                )
                .Metadata.SetValueComparer(
                    new ValueComparer<Dictionary<string, JsonElement>>(
                        (d1, d2) => JsonSerializer.Serialize(d1, (JsonSerializerOptions?)null)
                                 == JsonSerializer.Serialize(d2, (JsonSerializerOptions?)null),
                        d => JsonSerializer.Serialize(d, (JsonSerializerOptions?)null).GetHashCode(),
                        d => JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(
                            JsonSerializer.Serialize(d, (JsonSerializerOptions?)null),
                            (JsonSerializerOptions?)null)!
                    )
                );

            builder.HasOne(pm => pm.Tenant)
                .WithMany(t => t.PaymentMethods)
                .HasForeignKey(x => x.TenantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
