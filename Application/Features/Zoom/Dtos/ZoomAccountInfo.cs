namespace Application.Features.Zoom.Dtos
{
    public sealed class ZoomAccountInfo
    {
        public string ZoomUserId { get; set; } = string.Empty;
        public string ZoomEmail { get; set; } = string.Empty;
        public string ZoomDisplayName { get; set; } = string.Empty;
        public string ZoomAccountType { get; set; } = string.Empty;
        public string ZoomAccountTypeName { get; set; } = string.Empty;
        public DateTime? ConnectedAt { get; set; }
        public DateTime? LastUsedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
