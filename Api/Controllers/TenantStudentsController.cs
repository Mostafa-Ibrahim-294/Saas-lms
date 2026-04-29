using Application.Constants;
using Application.Features.Students.Commands.SendReminder;
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
        [HttpPost("send-reminder")]
        public async Task<IActionResult> SendReminder([FromBody] SendReminderCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
