using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Lessons.Dtos
{
    public sealed class ViewsOverTime
    {
        public DateTime Date { get; set; }
        public int TotalViews { get; set; }
    }
}
