using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Assignments.Dtos
{
    public sealed class OverviewDto
    {
        public int TotalSubmissions { get; set; }
        public double AverageScore { get; set; }
        public double HeighestScore { get; set; }
        public double LowestScore { get; set; }
    }
}
