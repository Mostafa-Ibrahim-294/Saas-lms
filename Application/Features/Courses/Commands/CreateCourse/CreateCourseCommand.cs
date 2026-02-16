using Application.Features.Courses.Dtos;
using Domain.Enums;

namespace Application.Features.Courses.Commands.CreateCourse
{
    public sealed record CreateCourseCommand(string Title, string Description, int GradeId, int SubjectId, string[]? Tags, string Thumbnail, string? Curriculum,
        decimal Price, PricingType PriceType, string Currency, byte Discount, CourseStatus Status, string Year, string? Video, string? Semester)
         : IRequest<OneOf<SuccessDto, Error>>;
}
