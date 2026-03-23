using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Courses.Dtos
{
    public sealed class CourseModuleDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public CourseStatus Status { get; set; }
        public int TotalItems { get; set; }
        public int Lessons { get; set; }
        public int Assignments { get; set; }    
        public int Quizzes { get; set; }
    }
}
