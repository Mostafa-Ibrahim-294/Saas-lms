namespace Infrastructure.Common.Options
{
    public sealed class BunnyOptions
    {
        public string AccessKey { get; set; } = string.Empty;
        public string StorageZoneName { get; set; } = string.Empty;
        public string HostName { get; set; } = string.Empty;
        public string CdnUrl { get; set; } = string.Empty;
        public string VideoLibraryId { get; set; } = string.Empty;
        public string StreamKey { get; set; } = string.Empty;
    }
}
