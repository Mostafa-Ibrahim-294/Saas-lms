namespace Application.Features.TenantStudents.Dtos
{
    public sealed class StudentsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
        public int AverageGrades { get; set; }
        public DateTime LastLogin { get; set; }
        public StudentFlagDto Flags { get; set; } = new();
        public List<int>? EnrolledCourses { get; set; } = new();
    }
}