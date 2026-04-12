namespace Application.Features.StudentAuth.Commands.ResendOtp
{
    public sealed record ResendOtpCommand : IRequest<OneOf<bool, Error>>;
}