using Application.Features.Plan.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Plans : ControllerBase
    {
        private readonly IMediator _mediator;

        public Plans(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllPlansQuery());
            return Ok(result);
        }
    }
}
