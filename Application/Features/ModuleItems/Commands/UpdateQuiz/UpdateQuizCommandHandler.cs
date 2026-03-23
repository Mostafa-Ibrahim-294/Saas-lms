using Application.Contracts.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ModuleItems.Commands.UpdateQuiz
{
    internal sealed class UpdateQuizCommandHandler : IRequestHandler<UpdateQuizCommand, OneOf<SuccessDto, Error>>
    {
        private readonly ITenantMemberRepository _tenantMemberRepository;
        private readonly ITenantRepository _tenantRepository;
        private readonly ICurrentUserId _currentUserId;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IModuleItemRepository _moduleItemRepository;
        private readonly IMapper _mapper;
        public UpdateQuizCommandHandler(ITenantMemberRepository tenantMemberRepository, ICurrentUserId currentUserId,
            ISubscriptionRepository subscriptionRepository, IHttpContextAccessor httpContextAccessor, ICourseRepository courseRepository,
            IMapper mapper, IModuleRepository moduleRepository, IModuleItemRepository moduleItemRepository, ITenantRepository tenantRepository)
        {
            _tenantMemberRepository = tenantMemberRepository;
            _currentUserId = currentUserId;
            _subscriptionRepository = subscriptionRepository;
            _httpContextAccessor = httpContextAccessor;
            _courseRepository = courseRepository;
            _mapper = mapper;
            _moduleRepository = moduleRepository;
            _moduleItemRepository = moduleItemRepository;
            _tenantRepository = tenantRepository;
        }
        public async Task<OneOf<SuccessDto, Error>> Handle(UpdateQuizCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserId.GetUserId();
            var subdomain = _httpContextAccessor?.HttpContext?.Request.Cookies[AuthConstants.SubDomain];
            var isPermitted = await _tenantMemberRepository.IsPermittedMember(userId, PermissionConstants.MANAGE_MODULE_ITEMS, cancellationToken);
            if (!isPermitted)
            {
                return MemberErrors.NotAllowed;
            }
            var isSubscribed = await _subscriptionRepository.HasActiveSubscriptionByTenantDomain(subdomain!, cancellationToken);
            if (!isSubscribed)
            {
                return TenantErrors.NotSubscribed;
            }
            var course = await _courseRepository.GetCourseByIdAsync(request.CourseId, subdomain!, cancellationToken);
            if (course is null)
            {
                return CourseErrors.CourseNotFound;
            }
            var module = await _moduleRepository.GetModuleByIdAsync(request.ModuleId, cancellationToken);
            if (module is null)
            {
                return ModuleErrors.ModuleNotFound;
            }
            var quiz = await _moduleItemRepository.GetQuizAsync(request.ItemId, cancellationToken);
            if (quiz is null)
            {
                return ModuleItemErrors.ModuleItemNotFound;
            }
            _mapper.Map(request, quiz);
            var tenantId = await _tenantRepository.GetTenantIdAsync(subdomain!, cancellationToken);
            var quizQuestions = _mapper.Map<List<QuizQuestion>>(request.Questions);
            foreach (var quizQuestion in quizQuestions)
            {
                quizQuestion.QuizId = quiz.ModuleItemId;
                quizQuestion.Question.TenantId = tenantId;
            }
            await _moduleItemRepository.AddTenantQuestionsAsync(quiz.ModuleItemId, quizQuestions, cancellationToken);
            return new SuccessDto
            {
                Id = quiz.ModuleItemId.ToString(),
                Message = $"{nameof(Quiz)} {SuccessConstatns.ItemUpdated}"
            };
        }
    }
}
