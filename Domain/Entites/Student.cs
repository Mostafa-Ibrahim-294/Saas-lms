using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entites
{
    public sealed class Student : IAuditable
    {
        public int Id { get; set; }
        public string ParentName { get; set; } = string.Empty;
        public string ParentEmail { get; set; } = string.Empty;
        public string ParentPhone { get; set; } = string.Empty;
        public int GradeId { get; set; }
        public Grade Grade { get; set; } = null!;
        public int TeachingLevelId { get; set; }
        public TeachingLevel TeachingLevel { get; set; } = null!;
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;
        public ICollection<Enrollment> Enrollments { get; set; } = [];
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

    }
}
