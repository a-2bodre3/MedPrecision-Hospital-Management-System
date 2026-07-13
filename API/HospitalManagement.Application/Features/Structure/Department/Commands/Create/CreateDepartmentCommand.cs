using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Department.Commands.Create
{
    public record CreateDepartmentCommand : IRequest<bool>
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int BranchId { get; set; }
    }
}
