namespace Application.Features.TenantAuth.Commands.VerifyOtp
{
    public sealed record VerifyOtpCommand(string OtpCode)
        : IRequest<OneOf<bool, Error>>;
}
