using Application.Features.ModuleItems.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ModuleItems.Queries.GetItem
{
    public sealed record GetItemQuery(int CourseId, int ModuleId, int ItemId) : IRequest<OneOf<ItemDto, Error>>;

}
