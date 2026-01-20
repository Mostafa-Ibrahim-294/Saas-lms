using Application.Features.Plan.DTOs;
using Microsoft.Extensions.Caching.Hybrid;
using AutoMapper;
using MediatR;
using Application.Contracts.Repositories;
using Application.Constants;
using Application.Features.Plans.Dtos;

namespace Application.Features.Plans.Queries.GetAllPlans
{
    public sealed class GetAllPlansQueryHandler : IRequestHandler<GetAllPlansQuery, PlanResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IPlanRepository _planRepository;
        private readonly HybridCache _hybridCache;

        public GetAllPlansQueryHandler(IMapper mapper , IPlanRepository planRepository , HybridCache hybridCache)
        {
            _mapper = mapper;
            _planRepository = planRepository;
            _hybridCache = hybridCache;
        }

        public async Task<PlanResponseDto> Handle(GetAllPlansQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = CacheKeysConstants.PlanKey;

            var plans = await _hybridCache.GetOrCreateAsync(
                cacheKey,
                async cacheEntry =>
                {
                    var data = await _planRepository.GetAllPlansWithDetailsAsync(cancellationToken);
                    return new PlanResponseDto { Plans = data.ToList() };
                },
                cancellationToken: cancellationToken
            );
            return plans;
        }
    }
}
