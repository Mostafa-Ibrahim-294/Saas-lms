namespace Application.Features.TenantMembers.Dtos
{
    public sealed class AcceptTenanInviteDto
    {
        public string Message { get; init; } = string.Empty;
        public string Subdomain { get; init; } = string.Empty;
    }
}
