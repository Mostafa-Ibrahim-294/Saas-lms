using Application.Features.Courses.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Courses.Queries
{
    internal sealed class GetStatisticsQueryHandler : IRequestHandler<GetStatisticsQuery, StatisticsDto>
    {
        public async Task<StatisticsDto> Handle(GetStatisticsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
