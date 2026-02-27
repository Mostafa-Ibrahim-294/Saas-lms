using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entites
{
    public sealed class Lesson
    {
        public string VideoId { get; set; } = null!;
        public File File { get; set; } = null!;
        public int ModuleItemId { get; set; }
        public ModuleItem ModuleItem { get; set; } = null!;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public int ModuleId { get; set; }
        public Module Module { get; set; } = null!;
        public List<Resource> Resources { get; set; } = [];
    }
    public sealed class Resource
    {
        public string Name { get; set; } = null!;
        public string Url { get; set; } = null!;
    }
}
