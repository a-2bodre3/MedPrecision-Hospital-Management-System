using HospitalManagement.Application.Features.Structure.Branch.Commands.Create;
using HospitalManagement.Application.Features.Structure.Branch.Commands.Delete;
using HospitalManagement.Application.Features.Structure.Branch.Commands.Update;
using HospitalManagement.Application.Features.Structure.Branch.Queries.GetAll;
using HospitalManagement.Application.Features.Structure.Branch.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HospitalManagement.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BranchController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Policy = "Branch_Read")]
        [HttpGet]
        public async Task<ActionResult<List<BranchResponse>>> GetAll()
        {
            var query = new GetAllBranchesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [Authorize(Policy = "Branch_Read")]
        [HttpGet("{id}")]
        public async Task<ActionResult<BranchDetailsResponse>> GetById(int id)
        {
            var query = new GetBranchByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize(Policy = "Branch_Create")]
        [HttpPost()]
        public async Task<ActionResult<bool>> Create([FromBody] CreateBranchCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [Authorize(Policy = "Branch_Update")]
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] UpdateBranchCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [Authorize(Policy = "Branch_Update")]
        [HttpDelete("{id}")]

        public async Task<ActionResult<bool>> Delete(int id)
        {
            var command = new DeleteBranchCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
