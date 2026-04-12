namespace Application.Features.Students.Dtos
{
    public sealed class ValidateStudentInviteDto
    {
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string InviterName { get; set; } = string.Empty;
        public bool IsValid { get; set; }
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = string.Empty;
        public bool IsExpired { get; set; }
    }
}