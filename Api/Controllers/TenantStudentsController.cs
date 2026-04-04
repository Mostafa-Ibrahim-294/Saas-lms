using Application.Constants;
using Application.Features.TenantStudents.Commands.DeclineInvite;
using Application.Features.TenantStudents.Commands.AcceptInvite;
using Application.Features.TenantStudents.Commands.DeleteStudent;
using Application.Features.TenantStudents.Commands.InviteStudent;
using Application.Features.TenantStudents.Commands.ValidateStudentInvite;
using Application.Features.TenantStudents.Queries.GetStudentsByCourseId;
using Application.Features.TenantStudents.Queries.GetStudentStatistics;
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

        public TenantStudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet()]
        public async Task<IActionResult> GetStudents([FromQuery] GetStudentsByCourseIdQuery query, CancellationToken cancellationToken)
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


        [HttpPost("invites/validate")]
        public async Task<IActionResult> ValidateInvite([FromQuery] ValidateStudentInviteCommand command, CancellationToken cancellationToken)
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


        [HttpPost("invites/accept")]
        public async Task<IActionResult> AcceptInvite([FromQuery] AcceptInviteCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.Match<IActionResult>(
                response => Ok(response),
                error => StatusCode((int)error.HttpStatusCode, error.Message)
            );
        }


        [HttpPost("invites/decline")]
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