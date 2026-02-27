using Application.Features.ModuleItems.Dtos;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ModuleItems.Queries.GetAll
{
    public sealed record GetAllItemsQuery(int CourseId, int ModuleId, ModuleItemType? Type) : IRequest<OneOf<IEnumerable<AllItemsDto>, Error>>;
}
