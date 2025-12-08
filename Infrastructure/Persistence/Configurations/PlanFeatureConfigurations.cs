using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class PlanFeatureConfigurations : IEntityTypeConfiguration<PlanFeature>
    {
        public void Configure(EntityTypeBuilder<PlanFeature> builder)
        {
            builder.HasKey(pf => pf.Id);

            builder.Property(pf => pf.LimitValue)
                    .IsRequired();

            builder.Property(pf => pf.LimitUnit)
                    .HasMaxLength(50)
                    .IsRequired();

            builder.Property(pf => pf.Note)
                   .HasMaxLength(500);

            builder.HasOne(pf => pf.Plan)
                   .WithMany(p => p.PlanFeatures)
                   .HasForeignKey(pf => pf.PlanId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pf => pf.Feature)
                   .WithMany(f => f.PlanFeatures)
                   .HasForeignKey(pf => pf.FeatureId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(pf => new { pf.PlanId, pf.FeatureId })
                  .IsUnique();
        }
    }
}
