using Application.Constants;
<<<<<<< HEAD
using Application.Features.Students.Commands.SendReminder;
=======
using Application.Features.TenantStudents.Commands.DeleteStudent;
using Application.Features.TenantStudents.Commands.InviteStudent;
using Application.Features.TenantStudents.Queries.GetStudent;
using Application.Features.TenantStudents.Queries.GetStudentsByCourseId;
using Application.Features.TenantStudents.Queries.GetStudentStatistics;
>>>>>>> 4c7a93aa4a11710a64ff2df81ec9e472ae2910a1
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/tenant/students")]
    [ApiController]
    [Authorize(AuthenticationSchemes = AuthConstants.ApiScheme)]
    public class TenantStudentsController : ControllerBase
    {
        private readonly IMediator _mediator;
<<<<<<< HEAD
=======

>>>>>>> 4c7a93aa4a11710a64ff2df81ec9e472ae2910a1
        public TenantStudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }
<<<<<<< HEAD
        [HttpPost("send-reminder")]
        public async Task<IActionResult> SendReminder([FromBody] SendReminderCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
=======

        [HttpGet()]
        public async Task<IActionResult> GetStudents([FromQuery] GetStudentsQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result.Match<IActionResult>(
                student => Ok(student),
                error => StatusCode((int)error.HttpStatusCode, error.Message)
            );
        }


        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetStudentById([FromRoute] GetStudentQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result.Match<IActionResult>(
                student => Ok(student),
                error => StatusCode((int)error.HttpStatusCode, error.Message)
            );
        }


        [HttpGet("statistics")]
        public async Task<IActionResult> GetStudentStatistics(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetStudentStatisticsQuery(), cancellationToken));
        }


        [HttpDelete()]
        public async Task<IActionResult> DeleteStudent([FromQuery] DeleteStudentCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.Match<IActionResult>(
                response => Ok(response),
                error => StatusCode((int)error.HttpStatusCode, error.Message)
            );
        }


        [HttpPost("invite")]
        public async Task<IActionResult> InviteStudent([FromBody] InviteStudentCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.Match<IActionResult>(
                response => Ok(response),
                error => StatusCode((int)error.HttpStatusCode, error.Message)
            );
        }
    }
}
>>>>>>> 4c7a93aa4a11710a64ff2df81ec9e472ae2910a1
