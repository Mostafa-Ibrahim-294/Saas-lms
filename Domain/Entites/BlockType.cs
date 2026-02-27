namespace Domain.Entites
{
    public sealed class BlockType
    {
        public string Id { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public Dictionary<string, object> Schema { get; set; } = [];
        public ICollection<PageBlock> PageBlocks { get; set; } = [];
    }
}