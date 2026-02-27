using Application.Features.ModuleItems.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ModuleItems.Queries.GetAssignment
{
    public sealed record GetAssignmentQuery(int CourseId, int ModuleId, int ItemId) : IRequest<OneOf<AssignmentDto, Error>>;
}
