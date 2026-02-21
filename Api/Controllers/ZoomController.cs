using Application.Constants;
using Application.Features.Tenants.Commands.CreateLiveSession;
using Application.Features.Zoom.Commands.Callback;
using Application.Features.Zoom.Queries.ConnectZoom;
using Application.Features.Zoom.Queries.GetZoomStatus;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Api.Controllers
{
    [Route("api/tenant/integrations/zoom")]
    [ApiController]
    [Authorize(AuthenticationSchemes = AuthConstants.ApiScheme)]
    public class ZoomController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ZoomController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("connect")]
        public async Task<IActionResult> Connect(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ConnectZoomQuery(), cancellationToken);
            return result.Match(
                success => Ok(success),
                error => StatusCode((int)error.HttpStatusCode, error.Message)
            );
        }


        [HttpGet("callback")]
        public async Task<IActionResult> Callback([FromQuery] CallbackCommand command, CancellationToken cancellationToken)
        {
            var redirectUrl = await _mediator.Send(new CallbackCommand(command.Code, command.State), cancellationToken);
            return Redirect(redirectUrl);
        }


        [HttpGet("status")]
        public async Task<IActionResult> GetZoomStatus(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetZoomStatusQuery(), cancellationToken));
        }
    }
}