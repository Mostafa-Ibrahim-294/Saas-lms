using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entites
{
    public sealed class ModuleItemCondition
    {
        public int Id { get; set; }
        public ConditionType ConditionType { get; set; }
        public ConditionEffect Effect { get; set; }
        public bool Enabled { get; set; }
        public string Message { get; set; } = null!;
        public int? Value { get; set; }
        public int ModuleItemId { get; set; }
        public ModuleItem ModuleItem { get; set; } = null!;
        public int? RequiredModuleItemId { get; set; }
        public ModuleItem? RequiredModuleItem { get; set; }
    }
}
