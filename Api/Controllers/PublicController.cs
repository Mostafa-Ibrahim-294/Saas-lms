using Application.Features.Public.Queries.GetCourseDetails;
using Application.Features.Public.Queries.GetTenantNavigationLinks;
using Application.Features.Public.Queries.GetTenantPages;
using Application.Features.Public.Queries.GetTenantPaymentMethods;
using Application.Features.Public.Queries.GetTenantSettings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/tenant/website/[controller]")]
    [ApiController]
    public class PublicController : ControllerBase
    {
        private readonly IMediator _mediator;
        private string GetSubDomain()
        {
            var origin = Request.Headers["Origin"].ToString();
            if (!string.IsNullOrEmpty(origin) && Uri.TryCreate(origin, UriKind.Absolute, out var uri))
                return uri.Host.Split('.')[0];

            return Request.Host.Host.Split('.')[0];
        }

        public PublicController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("payment-methods")]
        public async Task<IActionResult> GetPaymentMethods(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetTenantPaymentMethodsQuery(GetSubDomain()), cancellationToken));
        }


        [HttpGet("pages/navigation")]
        public async Task<IActionResult> GetTenantNavigationLinks(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetTenantNavigationLinksQuery(GetSubDomain()), cancellationToken));
        }


        [HttpGet("pages")]
        public async Task<IActionResult> GetTenantPages([FromQuery] string url, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetTenantPagesQuery(url, GetSubDomain()), cancellationToken);
            return result.Match<IActionResult>(
                success => Ok(success),
                error => StatusCode((int)error.HttpStatusCode, error.Message)
            );
        }


        [HttpGet("settings")]
        public async Task<IActionResult> GetTenantSettings(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetTenantSettingsQuery(GetSubDomain()), cancellationToken));
        }


        [HttpGet("courses/{courseId}")]
        public async Task<IActionResult> GetCourseDetails([FromRoute] int courseId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCourseDetailsQuery(courseId, GetSubDomain()), cancellationToken);
            return result.Match<IActionResult>(
                success => Ok(success),
                error => StatusCode((int)error.HttpStatusCode, error.Message)
            );
        }
    }
}