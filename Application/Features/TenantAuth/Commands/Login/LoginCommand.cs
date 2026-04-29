namespace Application.Features.TenantAuth.Commands.Login
{
    public sealed record LoginCommand(string Email, string Password)
        : IRequest<OneOf<LoginDto, Error>>;
}
