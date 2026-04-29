namespace Application.Features.TenantAuth.Commands.ResetPassword
{
    public sealed record ResetPasswordCommand(string resetToken, string Password)
        : IRequest<OneOf<bool, Error>>;
}
