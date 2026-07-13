using HospitalManagement.Application.DTO.Common;
using HospitalManagement.Application.Features.Identity.Users.ChangePassword;
using HospitalManagement.Application.Features.Staff.Employee.Commands.Create;
using HospitalManagement.Application.Features.Staff.Employee.Commands.Delete;
using HospitalManagement.Application.Features.Staff.Employee.Commands.Update;
using HospitalManagement.Application.Features.Staff.Employee.Queries.GetAll;
using HospitalManagement.Application.Features.Staff.Employee.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize("User_Read")]
        [HttpGet]
        public async Task<ActionResult<PagedResult<EmployeeResponse>>> GetAllEmployees([FromQuery] EmployeeQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize("User_Read")]
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDetailsResponse>> GetEmployeeById(int id)
        {
            var query = new EmployeeDetailsQuery { EmployeeId = id };
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [Authorize("User_Create")]
        [HttpPost]
        public async Task<ActionResult<bool>> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [Authorize("User_Update")]
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateEmployee(int id, [FromBody] UpdateEmployeeCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [Authorize("User_Update")]
        [HttpPatch("ChangePassword/{id}")]
        public async Task<ActionResult<bool>> ChangeEmployeePassword(int id, [FromBody] ChangePasswordCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [Authorize("User_Delete")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteEmployee(int id)
        {
            var command = new DeleteEmployeeCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
