using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Questions.Commands.DuplicateQuizQuestion
{
    public sealed record DuplicateQuizQuestionCommand(int CourseId, int ModuleId, int ItemId, int QuestionId) : IRequest<OneOf<SuccessDto, Error>>;

}
