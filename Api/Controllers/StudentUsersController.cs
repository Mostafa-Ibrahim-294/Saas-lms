using Application.Features.StudentUsers.Commands.Onboarding;
using Application.Features.StudentUsers.Queries.GetProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/student")]
    [ApiController]
    [Authorize]
    public class StudentUsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentUsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("users/me")]
        public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetProfileQuery(), cancellationToken);
            return result.Match<IActionResult>(
                profile => Ok(profile),
                error => StatusCode((int)error.HttpStatusCode, error.Message)
            );
        }


        [HttpPost("onboarding")]
        public async Task<IActionResult> Onboarding([FromBody] OnboardingCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.Match<IActionResult>(
                response => Ok(response),
                error => StatusCode((int)error.HttpStatusCode, error.Message)
            );
        }
    }
}