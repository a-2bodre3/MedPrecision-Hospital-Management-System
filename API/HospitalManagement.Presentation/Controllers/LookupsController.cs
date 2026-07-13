using HospitalManagement.Application.Features.Lookups.Queries.GetAppLookups;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LookupsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LookupsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<AppLookupsResponse>> GetAppLookups()
        {
            var query = new LookupsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
