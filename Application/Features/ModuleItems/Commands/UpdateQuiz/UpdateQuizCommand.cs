using Application.Features.ModuleItems.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ModuleItems.Commands.UpdateQuiz
{
    public sealed record UpdateQuizCommand(int ModuleId, int CourseId, int ItemId, int Duration, int PassingScore, bool ShowCorrectAnswers, bool ShuffleQuestions, TimeOnly StartTime,
         TimeOnly EndTime, DateOnly StartDate, DateOnly EndDate, IEnumerable<QuestionDto> Questions) : IRequest<OneOf<SuccessDto, Error>>;
}
