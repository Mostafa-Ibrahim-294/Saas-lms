using Domain.Abstractions;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entites
{
    public sealed class Course : IAuditable
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public string? Curriculum { get; set; }
        public decimal Price { get; set; }
        public string? VideoUrl { get; set; }
        public string ThumbnailUrl { get; set; } = string.Empty;
        public byte Discount { get; set; }
        public string Year { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow).Year.ToString();
        public string? Semester { get; set; }
        public CourseStatus CourseStatus { get; set; }
        public PricingType PricingType { get; set; }
        public BillingCycle? BillingCycle { get; set; }
        public string[]? Tags { get; set; }
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;
        public string CreatedById { get; set; } = string.Empty;
        public ApplicationUser CreatedBy { get; set; } = null!;
        public int SubjectId { get; set; }
        public Subject Subject { get; set; } = null!;
        public int GradeId { get; set; }
        public Grade Grade { get; set; } = null!;
        public ICollection<Enrollment> Enrollments { get; set; } = [];
        public ICollection<CourseProgress> CourseProgresses { get; set; } = [];
        public ICollection<Module> Modules { get; set; } = [];
        public ICollection<Lesson> Lessons { get; set; } = [];
        public ICollection<ModuleItem> ModuleItems { get; set; } = [];
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public ICollection<LiveSession> LiveSessions { get; set; } = [];
        public ICollection<Order> Orders { get; set; } = [];
    }
}
