using Application.Features.StudentUsers.Dtos;

namespace Application.Features.StudentUsers.Queries.GetProfile
{
    public sealed record GetProfileQuery : IRequest<OneOf<StudentUserProfileDto, Error>>;
}