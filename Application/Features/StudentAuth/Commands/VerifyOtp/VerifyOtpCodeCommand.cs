namespace Application.Features.StudentAuth.Commands.VerifyOtp
{
    public sealed record VerifyOtpCodeCommand(string OtpCode) : IRequest<OneOf<bool, Error>>;
}