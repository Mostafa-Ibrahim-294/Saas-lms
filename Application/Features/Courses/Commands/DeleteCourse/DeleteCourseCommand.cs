using Application.Features.Courses.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Courses.Commands.DeleteCourse
{
    public sealed record DeleteCourseCommand(int CourseId)
         : IRequest<OneOf<CourseDto, Error>>;
}
