namespace Application.Features.Public.Dtos
{
    public sealed class TenantNavigationLinkDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public bool IsHomepage { get; set; }
    }
}
