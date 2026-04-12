using Application.Features.StudentUsers.Queries.GetProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/student/users/me")]
    [ApiController]
    [Authorize]
    public class StudentUsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentUsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet()]
        public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetProfileQuery(), cancellationToken);
            return result.Match<IActionResult>(
                profile => Ok(profile),
                error => StatusCode((int)error.HttpStatusCode, error.Message)
            );
        }
    }
}