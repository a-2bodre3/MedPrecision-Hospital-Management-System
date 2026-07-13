using HospitalManagement.Application.DTO.Common;
using HospitalManagement.Application.Features.Identity.Users.ChangePassword;
using HospitalManagement.Application.Features.Patient.Commands.Create;
using HospitalManagement.Application.Features.Patient.Commands.Delete;
using HospitalManagement.Application.Features.Patient.Commands.Update;
using HospitalManagement.Application.Features.Patient.Queries.GetAll;
using HospitalManagement.Application.Features.Patient.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize("Patient_Read_Basic")]
        [HttpGet]
        public async Task<ActionResult<PagedResult<PatientsResponse>>> GetAllPatient([FromQuery] PatientsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize("Patient_Read_Medical")]
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDetailsResponse>> GetPatientById(int id)
        {
            var query = new PatientDetailsQuery { PatientId = id };
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [Authorize("Patient_Create")]
        [HttpPost]
        public async Task<ActionResult<bool>> CreatePatient([FromBody] CreatePatientCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [Authorize("Patient_Update")]
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdatePatient(int id, [FromBody] UpdatePatientCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [Authorize("Patient_Update")]
        [HttpPatch("ChangePassword/{id}")]
        public async Task<ActionResult<bool>> ChangePatientPassword(int id, [FromBody] ChangePasswordCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [Authorize("Patient_Delete")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeletePatient(int id)
        {
            var command = new DeletePatientCommand { Id = id};
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
