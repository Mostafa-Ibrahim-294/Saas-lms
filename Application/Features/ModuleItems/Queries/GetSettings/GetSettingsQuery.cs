using Application.Features.ModuleItems.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ModuleItems.Queries.GetSettings
{
    public sealed record GetSettingsQuery(int CourseId, int ModuleId, int ItemId) : IRequest<OneOf<SettingsDto, Error>>;

}
