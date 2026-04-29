using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Questions.Commands.DeleteQuestion
{
    public sealed record DeleteQuestionCommand(int QuestionId) : IRequest<OneOf<bool, Error>>;
}
