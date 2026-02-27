using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entites
{
    public sealed class Assignment
    {
        public DateTime DueDate { get; set; }
        public string Instructions { get; set; } = null!;
        public int Marks { get; set; }
        public SubmissionType SubmissionType { get; set; }
        public List<Attachment> Attachments { get; set; } = [];
        public string CreatedById { get; set; } = null!;
        public ApplicationUser CreatedBy { get; set; } = null!;
        public int ModuleItemId { get; set; }
        public ModuleItem ModuleItem { get; set; } = null!;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public int ModuleId { get; set; }
        public Module Module { get; set; } = null!;


    }
    public sealed class Attachment
    {
        public string Name { get; set; } = null!;
        public string Url { get; set; } = null!;
    }
}
