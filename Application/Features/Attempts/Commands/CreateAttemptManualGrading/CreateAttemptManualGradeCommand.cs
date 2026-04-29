using Application.Features.Attempts.Dtos;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Attempts.Commands.CreateAttemptManualGrading
{
    public sealed record CreateAttemptManualGradeCommand(int QuizId, int AttemptId, List<QuestionDto> Questions, double OverallScore, GradingStatus Status)
        : IRequest<OneOf<bool, Error>>;
}
