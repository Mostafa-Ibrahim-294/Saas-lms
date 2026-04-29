using Application.Features.Announcements.Dtos;

namespace Application.Features.Announcements.Commands.DeleteAnnouncement
{
    public sealed record DeleteAnnouncementCommand(int AnnouncementId) : IRequest<OneOf<AnnouncementResponse, Error>>;
}