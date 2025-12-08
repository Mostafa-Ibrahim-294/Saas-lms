using Application.Contracts.Repositories.PlanRepository;
using Application.Features.Plan.DTOs;
using Microsoft.Extensions.Caching.Hybrid;
using AutoMapper;
using MediatR;

namespace Application.Features.Plan.Queries
{
    public sealed class GetAllPlansQueryHandler : IRequestHandler<GetAllPlansQuery, IEnumerable<PlanResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IPlanRepository _planRepository;
        private readonly HybridCache _hybridCache;

        public GetAllPlansQueryHandler(IMapper mapper , IPlanRepository planRepository , HybridCache hybridCache)
        {
            this._mapper = mapper;
            this._planRepository = planRepository;
            this._hybridCache = hybridCache;
        }

        public async Task<IEnumerable<PlanResponse>> Handle(GetAllPlansQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = "Plans";

            var plans = await _hybridCache.GetOrCreateAsync(
                cacheKey,
                async cacheEntry =>
                {
                    var data = await _planRepository.GetAllPlansWithDetailsAsync();
                    return _mapper.Map<IEnumerable<PlanResponse>>(data);
                },
                cancellationToken: cancellationToken
            );

            return plans;
        }
    }
}
