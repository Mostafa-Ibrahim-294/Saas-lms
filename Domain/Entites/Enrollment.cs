using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entites
{
    public sealed class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public EnrollmentType EnrollmentType { get; set; }
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; }
    }
}
