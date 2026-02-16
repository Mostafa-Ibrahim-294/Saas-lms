using Application.Features.Roles.Dtos;

namespace Application.Features.Roles.Commands.UpdateRole
{
    public sealed record UpdateRoleCommand(int RoleId, string Name, string Description, List<string> EnabledPermissions)
        : IRequest<OneOf<RoleDto, Error>>;
}
