namespace Application.Features.StudentAuth.Commands.Login
{
    public sealed record LoginCommand(string Email) : IRequest<OneOf<bool, Error>>;
}