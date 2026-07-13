using HospitalManagement.Application.Features.Identity.Users.ChangePassword;
using HospitalManagement.Application.Features.Staff.Doctor.Commands.Create;
using HospitalManagement.Application.Features.Staff.Doctor.Commands.Update;
using HospitalManagement.Application.Features.Staff.Doctor.Queries.GetDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DoctorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize("User_Read")]
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDetailsResponse>> GetDoctorById(int id)
        {
            var query = new DoctorDetailsQuery { DoctorId = id };
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [Authorize("User_Create")]
        [HttpPost]
        public async Task<ActionResult<bool>> CreateDoctor([FromBody] CreateDoctorCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize("User_Update")]
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateDoctor(int id, [FromBody] UpdateDoctorCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize("User_Update")]
        [HttpPatch("changePassword/{id}")]
        public async Task<ActionResult<bool>> ChangeDoctorPassword(int id, [FromBody] ChangePasswordCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
