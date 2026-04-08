using Application.Features.Announcements.Dtos;
using Domain.Enums;

namespace Application.Features.Announcements.Commands.CreateAnnouncement
{
    public sealed record CreateAnnouncementCommand(string Title, string Content, AnnouncementType TargetType, bool IsPinned, int[]? TargetCourseIds)
        : IRequest<AnnouncementResponse>;
}