namespace Application.Constants
{
    public static class ZoomConstants
    {
        public const string Code = "code";
        public const string RedirectUri = "redirect_uri";
        public const string Bearer = "Bearer";

        public const string AuthorizationUrl = "https://zoom.us/oauth/authorize";
        public const string TokenUrl = "https://zoom.us/oauth/token";
        public const string ZoomUserMe = "https://api.zoom.us/v2/users/me";
        public const string MeetingRequest = "https://api.zoom.us/v2/users/me/meetings";
        public const string ZoomMeetingsUrl = "https://api.zoom.us/v2/meetings/";
        public const string FrontendRedirectUrl = ".waey.online/dashboard/live-sessions";

        // Webhook Events
        public const string MeetingStarted = "meeting.started";
        public const string MeetingEnded = "meeting.ended";
        public const string ParticipantJoined = "meeting.participant_joined";
        public const string ParticipantLeft = "meeting.participant_left";
        public const string UrlValidation = "endpoint.url_validation";
    }
}
