namespace Application.Features.TenantMembers.Dtos
{
    public sealed class MemberProfileDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public int ExperienceYears { get; set; }
        public string Role { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public List<string> Permissions { get; set; } = [];
        public string Bio { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
    }
}
