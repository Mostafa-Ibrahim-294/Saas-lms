using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Questions.Commands.ReorderQuestion
{
    public sealed record ReorderQuestionCommand(int CourseId, int ModuleId, int ItemId, IEnumerable<int> QuestionIds) : IRequest<OneOf<bool, Error>>;
}
