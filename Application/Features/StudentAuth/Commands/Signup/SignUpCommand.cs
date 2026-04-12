namespace Application.Features.StudentAuth.Commands.Signup
{
    public sealed record SignUpCommand(string FirstName, string LastName, string Email, string PhoneNumber, string ParentEmail)
        : IRequest<OneOf<bool, Error>>;
}