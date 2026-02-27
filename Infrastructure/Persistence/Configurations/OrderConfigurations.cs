using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.PricePaid)
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.OrderNumber)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(x => x.OrderNumber).IsUnique();

            builder.HasOne(o => o.Tenant)
                .WithMany(t => t.Orders)
                .HasForeignKey(o => o.TenantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Course)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Student)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
