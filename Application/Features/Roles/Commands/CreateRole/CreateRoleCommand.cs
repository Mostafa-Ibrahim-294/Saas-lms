using Application.Features.Roles.Dtos;

namespace Application.Features.Roles.Commands.CreateRole
{
    public sealed record CreateRoleCommand(string Name, string Description, List<string> EnabledPermissions)
        : IRequest<OneOf<RoleDto, Error>>;
}
