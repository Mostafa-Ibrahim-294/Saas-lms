namespace Application.Features.Zoom.Commands.Callback
{
    public sealed record CallbackCommand(string Code, string State): IRequest<string>;
}
