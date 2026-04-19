using Application.Features.StudentLessons.Queries.GetStudentDiscussions;
using Application.Features.StudentLessons.Queries.GetStudentLessonItem;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/students/courses")]
    [ApiController]
    public class StudentLessonsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentLessonsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{courseId}/items/{itemId}/lesson")]
        public async Task<IActionResult> GetStudentLessonItem([FromRoute] int courseId, [FromRoute] int itemId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetStudentLessonItemQuery(courseId, itemId), cancellationToken);
            return result.Match(
                lessonItem => Ok(lessonItem),
                error => StatusCode((int)error.HttpStatusCode, error.Message)
            );
        }


        [HttpGet("{courseId}/items/{itemId}/lesson/discussions")]
        public async Task<IActionResult> GetStudentDiscussions([FromRoute] int courseId, [FromRoute] int itemId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetStudentDiscussionsQuery(courseId, itemId), cancellationToken);
            return result.Match(
                lessonItem => Ok(lessonItem),
                error => StatusCode((int)error.HttpStatusCode, error.Message)
            );
        }
    }
}