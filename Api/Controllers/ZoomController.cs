using Application.Constants;
using Application.Features.Zoom.Commands.Callback;
using Application.Features.Zoom.Commands.Webhook;
using Application.Features.Zoom.Queries.ConnectZoom;
using Application.Features.Zoom.Queries.GetZoomStatus;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Api.Controllers
{
    [Route("api/tenant/integrations/zoom")]
    [ApiController]
    [Authorize(AuthenticationSchemes = AuthConstants.ApiScheme)]
    public class ZoomController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly string _secretToken;

        public ZoomController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _secretToken = configuration["ZoomOptions:SecretToken"] ?? string.Empty;
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
        [AllowAnonymous]
        public async Task<IActionResult> Callback([FromQuery] CallbackCommand command, CancellationToken cancellationToken)
        {
            var redirectUrl = await _mediator.Send(new CallbackCommand(command.code, command.state), cancellationToken);
            return Redirect(redirectUrl);
        }


        [HttpGet("status")]
        public async Task<IActionResult> GetZoomStatus(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetZoomStatusQuery(), cancellationToken));
        }


        [HttpPost("webhooks")]
        [AllowAnonymous]
        public async Task<IActionResult> Webhook(CancellationToken cancellationToken)
        {
            var body = await new StreamReader(Request.Body).ReadToEndAsync(cancellationToken);
            using var doc = JsonDocument.Parse(body);
            var root = doc.RootElement;
            if (root.TryGetProperty("event", out var eventProp) && eventProp.GetString() == ZoomConstants.UrlValidation)
            {
                var plainToken = root.GetProperty("payload").GetProperty("plainToken").GetString()!;
                return Ok(new { plainToken, encryptedToken = HashToken(plainToken, _secretToken) });
            }

            if (!VerifySignature(body))
                return Unauthorized();

            await _mediator.Send(new ZoomWebhookCommand(body), cancellationToken);
            return Ok();
        }

        private bool VerifySignature(string body)
        {
            if (string.IsNullOrEmpty(_secretToken))
                return true;

            var timestamp = Request.Headers["x-zm-request-timestamp"].FirstOrDefault();
            var signature = Request.Headers["x-zm-signature"].FirstOrDefault();

            if (string.IsNullOrEmpty(timestamp) || string.IsNullOrEmpty(signature))
                return false;

            var message = $"v0:{timestamp}:{body}";
            var hash = HashToken(message, _secretToken);
            var expectedSignature = $"v0={hash}";

            return string.Equals(signature, expectedSignature, StringComparison.OrdinalIgnoreCase);
        }
        private static string HashToken(string message, string secret)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
            return Convert.ToHexStringLower(hash);
        }
    }
}