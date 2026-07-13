using HospitalManagement.Application.Features.Structure.Room.Commands.Create;
using HospitalManagement.Application.Features.Structure.Room.Commands.Deletee;
using HospitalManagement.Application.Features.Structure.Room.Commands.Update;
using HospitalManagement.Application.Features.Structure.Room.Queries.GetAll;
using HospitalManagement.Application.Features.Structure.Room.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace HospitalManagement.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoomController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Policy = "Room_Read")]
        [HttpGet]
        public async Task<ActionResult<List<RoomResponse>>> GetAll()
        {
            var query = new RoomQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize(Policy = "Room_Read")]
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDetailsResponse>> GetById(int id)
        {
            var query = new RoomDetailsQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize(Policy = "Room_Management")]
        [HttpPost()]
        public async Task<ActionResult<bool>> Create([FromBody] CreateRoomCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Policy = "Room_Management")]
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Update(int id , [FromBody] UpdateRoomCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Policy = "Room_Management")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var command = new DeleteRoomCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
