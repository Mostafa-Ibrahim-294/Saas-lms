namespace Application.Features.TenantStudents.Dtos
{
    public sealed class ValidateStudentInviteDto
    {
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string InviterName { get; set; } = string.Empty;
        public bool IsValid { get; set; }
        public bool IsExpired { get; set; }
    }
}