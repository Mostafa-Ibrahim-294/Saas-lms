using Application.Features.Zoom.Dtos;

namespace Application.Features.Zoom.Queries.ConnectZoom
{
    public sealed record ConnectZoomQuery : IRequest<OneOf<ConnectZoomDto, Error>>;
}