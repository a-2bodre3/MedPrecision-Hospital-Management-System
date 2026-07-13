using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Department.Commands.Update
{
    public record UpdateDepartmentCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required bool IsActive { get; set; }
        public required int BranchId { get; set; }
    }
}
