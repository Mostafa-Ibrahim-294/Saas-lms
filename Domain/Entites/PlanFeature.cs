using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Domain.Entites
{
    public sealed class PlanFeature
    {
        public string Id { get; set; } = string.Empty;
        public int LimitValue { get; set; }
        public string LimitUnit { get; set; } = string.Empty;
        public string? Note { get; set; }


        public string PlanId { get; set; } = string.Empty;
        public Plan Plan { get; set; }

        public string FeatureId { get; set; } = string.Empty;
        public Feature Feature { get; set; }
    }
}
