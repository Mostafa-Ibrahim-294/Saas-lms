namespace Application.Features.Tenants.Dtos
{
    public sealed class TenantPermissionDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Module { get; set; } = string.Empty;
    }
}
