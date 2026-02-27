namespace Application.Features.TenantWebsite.Dtos
{
    public sealed class BlockTypeDto
    {
        public string Id { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Dictionary<string, object> Schema { get; set; } = new Dictionary<string, object>();
        public string Icon { get; set; } = string.Empty;
    }
}
