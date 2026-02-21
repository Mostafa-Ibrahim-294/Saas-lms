namespace Application.Features.Zoom.Dtos
{
    public sealed class GetZoomStatusResponse
    {
        public bool IsConnected { get; set; }
        public ZoomAccountInfo? AccountInfo { get; set; }
    }
}
