namespace Application.Features.StudentAuth.Commands.Logout
{
    public sealed record LogoutCommand : IRequest<OneOf<bool, Error>>;
}