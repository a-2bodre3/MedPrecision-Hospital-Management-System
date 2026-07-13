using HospitalManagement.Application.Features.Identity.Authorization.Permission.Queries.GetPermissions;
using HospitalManagement.Application.Features.Identity.Role.Commands.Create;
using HospitalManagement.Application.Features.Identity.Role.Commands.Delete;
using HospitalManagement.Application.Features.Identity.Role.Commands.Update;
using HospitalManagement.Application.Features.Identity.Role.Commands.UpdateRolePermissions;
using HospitalManagement.Application.Features.Identity.Role.Queries.GetRolePermissions;
using HospitalManagement.Application.Features.Identity.Role.Queries.GetRoles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Policy = "Role_Manage")]
        [HttpGet]
        public async Task<ActionResult<List<RoleResponse>>> GetRoles()
        {
            var query = new RolesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [Authorize(Policy = "Role_Manage")]
        [HttpGet("GetPermissions")]
        public async Task<ActionResult<List<PermissionsResponse>>> GetPermissions()
        {
            var query = new PermissionsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize(Policy = "Role_Manage")]
        [HttpGet("{id}/permissions")]
        public async Task<ActionResult<List<PermissionsResponse>>> GetRolePermissions(int id)
        {
            var query = new RolePermissionsQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [Authorize(Policy = "Role_Manage")]
        [HttpPost]
        public async Task<ActionResult<bool>> CreateRole([FromBody] CreateRoleCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Policy = "Role_Manage")]
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateRole(int id, [FromBody] UpdateRoleCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [Authorize(Policy = "Role_Manage")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteRole(int id)
        {
            var command = new DeleteRoleCommand { Id  = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [Authorize(Policy = "Role_Manage")]
        [HttpPatch("{id}")]
        public async Task<ActionResult<bool>> UpdateRolePermissions(int id, [FromBody] UpdateRolePermissionsCommand command)
        {
            command.RoleId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
