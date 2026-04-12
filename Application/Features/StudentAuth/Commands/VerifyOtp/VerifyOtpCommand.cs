using Application.Features.StudentAuth.Dtos;

namespace Application.Features.StudentAuth.Commands.VerifyOtp
{
    public sealed record VerifyOtpCommand(string OtpCode) : IRequest<OneOf<bool, Error>>;
}