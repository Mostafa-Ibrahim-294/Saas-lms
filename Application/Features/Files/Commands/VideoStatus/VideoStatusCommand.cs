namespace Application.Features.Files.Commands.VideoStatus
{
    public record VideoStatusCommand(string Id, string Status) : IRequest<Unit>{ }
}
