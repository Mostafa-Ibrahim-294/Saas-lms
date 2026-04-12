namespace Application.Features.TenantAuth.Commands.Signup
{
    public sealed record SignupCommand(string FirstName, string LastName, string Email, string Password, string? PhoneNumber)
        : IRequest<OneOf<bool, Error>>;
}
