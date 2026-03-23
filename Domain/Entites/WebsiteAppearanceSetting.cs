using Domain.Enums;

namespace Domain.Entites
{
    public sealed class WebsiteAppearanceSetting
    {
        public int Id { get; set; }
        public string? FavIcon { get; set; }
        public string PrimaryColor { get; set; } = string.Empty;
        public string SecondaryColor { get; set; } = string.Empty;
        public string FontFamily { get; set; } = string.Empty;
        public DirectionType Direction { get; set; }
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;
    }
}
