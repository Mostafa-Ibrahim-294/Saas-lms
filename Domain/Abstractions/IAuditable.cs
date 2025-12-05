using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstractions
{
    public interface IAuditable
    {
        public DateTime CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; }
    }
}
