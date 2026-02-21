using Application.Features.Tenants.Dtos;

namespace Application.Features.Tenants.Commands.DeleteLiveSession
{
    public sealed record DeleteLiveSessionCommand(int SessionId) : IRequest<OneOf<DeleteLiveSessionDto, Error>>;
}
