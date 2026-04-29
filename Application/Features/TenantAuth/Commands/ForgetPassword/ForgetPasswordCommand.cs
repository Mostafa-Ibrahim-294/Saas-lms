namespace Application.Features.TenantAuth.Commands.ForgetPassword
{
    public sealed record ForgetPasswordCommand(string Email)
        : IRequest<OneOf<bool, Error>>;
}
