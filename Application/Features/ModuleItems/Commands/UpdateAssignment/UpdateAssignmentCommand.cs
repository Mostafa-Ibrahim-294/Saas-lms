using Application.Features.ModuleItems.Dtos;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ModuleItems.Commands.UpdateAssignment
{
    public sealed record UpdateAssignmentCommand(int CourseId, int ModuleId, int ItemId, string Title,
        string? Description, DateTime DueDate, string Instructions, int TotalMarks, SubmissionType SubmissionType,
        IEnumerable<Attachment> Attachments) : IRequest<OneOf<SuccessDto, Error>>;
}
