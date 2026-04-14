using Application.Features.StudentCourse.Queries.GetStudentCourses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/student/courses")]
    [ApiController]
    [Authorize]
    public class StudentCoursesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StudentCoursesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet()]
        public async Task<IActionResult> GetStudentCourses(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetStudentCoursesQuery(), cancellationToken);
            return result.Match(
                courses => Ok(courses),
                error => StatusCode((int)error.HttpStatusCode, error.Message)
            );
        }
    }
}