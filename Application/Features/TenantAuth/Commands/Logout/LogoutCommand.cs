namespace Application.Features.TenantAuth.Commands.Logout
{
    public sealed record LogoutCommand : IRequest<OneOf<bool, Error>>;
}
