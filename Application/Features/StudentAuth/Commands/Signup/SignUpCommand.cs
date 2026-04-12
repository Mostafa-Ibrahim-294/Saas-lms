namespace Application.Features.StudentAuth.Commands.Signup
{
    public sealed record SignUpCommand(string FirstName, string LastName, string Email, string PhoneNumber)
        : IRequest<OneOf<bool, Error>>;
}