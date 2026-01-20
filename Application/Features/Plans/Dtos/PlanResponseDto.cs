using Application.Features.Plan.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Plans.Dtos
{
    public sealed class PlanResponseDto
    {
        public IEnumerable<PlanDto> Plans { get; set; } = Enumerable.Empty<PlanDto>();
    }
}
