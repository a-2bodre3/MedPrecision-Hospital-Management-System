using HospitalManagement.Application.Features.Structure.Department.Commands.Create;
using HospitalManagement.Application.Features.Structure.Department.Commands.Delete;
using HospitalManagement.Application.Features.Structure.Department.Commands.Update;
using HospitalManagement.Application.Features.Structure.Department.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Policy = "Department_Read")]
        [HttpGet]
        public async Task<ActionResult<List<DepartmentResponse>>> GetAll()
        {
            var query = new DepartmentQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize(Policy = "Department_Management")]
        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] CreateDepartmentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Policy = "Department_Management")]
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Update(int id , [FromBody] UpdateDepartmentCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Policy = "Department_Management")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var command = new DeleteDepartmentCommand { Id = id};
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
