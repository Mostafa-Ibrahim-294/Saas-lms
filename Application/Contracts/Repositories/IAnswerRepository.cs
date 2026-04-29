using Application.Features.Attempts.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Contracts.Repositories
{
    public interface IAnswerRepository
    {
        Task UpdateTeacherScore(int attemptId, List<QuestionDto> questions, CancellationToken cancellationToken);
    }
}
