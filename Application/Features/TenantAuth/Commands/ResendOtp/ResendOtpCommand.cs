namespace Application.Features.TenantAuth.Commands.ResendOtp
{
    public sealed record ResendOtpCommand : IRequest<OneOf<bool, Error>>;
}
