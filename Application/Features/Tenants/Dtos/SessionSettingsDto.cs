namespace Application.Features.Tenants.Dtos
{
    public sealed record SessionSettingsDto(bool EnableChat, bool ParticipantVideo, bool WaitingRoom);
}
