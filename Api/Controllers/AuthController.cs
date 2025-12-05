using Application.Features.Auth.Commands.Signup;
using Application.Features.Auth.Commands.VerifyOtp;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("signup")]
        public async Task<IActionResult> Signup(SignupCommand signupCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(signupCommand, cancellationToken);
            return result.Match(
                success => Created(),
                error => StatusCode((int)error.HttpStatusCode, error.Message)
            );
        }
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp(VerifyOtpCommand verifyOtpCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(verifyOtpCommand, cancellationToken);
            return result.Match(
                success => Ok(new { Message = "Success" }),
                error => StatusCode((int)error.HttpStatusCode, error.Message)
            );
        }

    }
}
