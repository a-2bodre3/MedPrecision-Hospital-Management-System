using HospitalManagement.Domain.Value_Objects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Branch.Commands.Update
{
    public class UpdateBranchCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public required Address Address { get; set; }
        public required bool IsActive { get; set; }
    }
}
