using Application.Features.Attempts.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Attempts.Queries.GetAttempt
{
    public sealed record GetAttemptQuery(int QuizId, int AttemptId) : IRequest<OneOf<AttemptResponse, Error>>;
}
