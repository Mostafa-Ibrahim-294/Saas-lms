namespace Application.Features.TenantMembers.Dtos
{
    public sealed class ValidateTenanInviteDto
    {
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string TenantName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string InviterName { get; set; } = string.Empty;
        public bool IsValid { get; set; }
        public bool IsExpired { get; set; }
        public string Subdomain { get; set; } = string.Empty;
        public List<string> RolePermissions { get; set; } = [];
    }
}
