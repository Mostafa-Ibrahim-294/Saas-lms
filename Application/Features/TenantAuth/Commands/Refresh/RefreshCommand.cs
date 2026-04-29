namespace Application.Features.TenantAuth.Commands.Refresh
{
    public sealed record RefreshCommand(string? RefreshToken)
        : IRequest<OneOf<bool, Error>>;
}
