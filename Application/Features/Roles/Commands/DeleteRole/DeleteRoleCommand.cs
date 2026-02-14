using Application.Features.Roles.Dtos;

namespace Application.Features.Roles.Commands.DeleteRole
{
    public sealed record DeleteRoleCommand(int RoleId) : IRequest<OneOf<RoleDto, Error>>;
}
