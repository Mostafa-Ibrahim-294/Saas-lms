using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class OrderTimeLineConfigurations : IEntityTypeConfiguration<OrderTimeLine>
    {
        public void Configure(EntityTypeBuilder<OrderTimeLine> builder)
        {
            builder.Property(x => x.Description)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.Actor)
                .HasMaxLength(100);

            builder.HasOne(ot => ot.Order)
                .WithMany(o => o.OrderTimeLines)
                .HasForeignKey(ot => ot.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
