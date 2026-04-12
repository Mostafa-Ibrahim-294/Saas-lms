using Application.Features.Students.Commands.AcceptInvite;
using Application.Features.Students.Commands.DeclineInvite;
using Application.Features.Students.Commands.ValidateStudentInvite;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/student/invites")]
    [ApiController]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("validate")]
        public async Task<IActionResult> ValidateInvite([FromQuery] ValidateStudentInviteCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.Match<IActionResult>(
                response => Ok(response),
                error => StatusCode((int)error.HttpStatusCode, error.Message)
            );
        }


        [HttpPost("accept")]
        public async Task<IActionResult> AcceptInvite([FromQuery] AcceptInviteCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.Match<IActionResult>(
                response => Ok(response),
                error => StatusCode((int)error.HttpStatusCode, error.Message)
            );
        }


        [HttpPost("decline")]
        public async Task<IActionResult> DeclineInvite([FromQuery] DeclineInviteCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.Match<IActionResult>(
                response => Ok(response),
                error => StatusCode((int)error.HttpStatusCode, error.Message)
            );
        }
    }
}
