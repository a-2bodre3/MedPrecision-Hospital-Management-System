using HospitalManagement.Application.DTO.Common;
using HospitalManagement.Application.Features.Scheduling.DoctorSchedule.Commands.AdjustScheduleValidity;
using HospitalManagement.Application.Features.Scheduling.DoctorSchedule.Commands.Delete;
using HospitalManagement.Application.Features.Scheduling.DoctorSchedule.Queries.GetAllDoctorSchedules;
using HospitalManagement.Application.Features.Scheduling.DoctorSchedule.Queries.GetDoctorSchedules;
using HospitalManagement.Application.Features.Sheduling.DoctorSchedule.Commands.Create;
using HospitalManagement.Application.Features.Staff.Doctor.Commands.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorScheduleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DoctorScheduleController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<PagedResult<DoctorSchedulesResponse>>> GetAllDoctorSchedule([FromQuery] DoctorSchedulesQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize(Policy = "Doctor_Schedule_Details")]
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorScheduleDetailsResponse>> GetDoctorScheduleById(int id)
        {
            var query = new DoctorScheduleDetailsQuery { DoctorScheduleId = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize(Policy = "Doctor_Schedule_Create")]
        [HttpPost]
        public async Task<ActionResult<bool>> CreateDoctorSchedule([FromBody] CreateDoctorScheduleCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Policy = "Doctor_Schedule_Update")]
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateDoctorSchedule(int id, [FromBody] UpdateDoctorCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Policy = "Doctor_Schedule_Update")]
        [HttpPatch("{id}")]
        public async Task<ActionResult<bool>> UpdateDoctorScheduleValidaty(int id, [FromBody] AdjustScheduleValidityCommand command)
        {
            command.DoctorScheduleId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Policy = "Doctor_Schedule_Delete")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteDoctorSchedule(int id)
        {
            var command = new DeleteDoctorScheduleCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
