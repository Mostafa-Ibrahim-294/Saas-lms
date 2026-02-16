using Application.Features.Courses.Dtos;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Courses.Commands.UpdateCourse
{
    public sealed record UpdateCourseCommand(int CourseId, string Title, string Description, int GradeId, int SubjectId, string[] Tags, string Thumbnail, string? Curriculum,
        decimal Price, PricingType PriceType, string Currency, byte Discount, CourseStatus Status, string Year, string? Video, string? Semester)
         : IRequest<OneOf<SuccessDto, Error>>;
}
