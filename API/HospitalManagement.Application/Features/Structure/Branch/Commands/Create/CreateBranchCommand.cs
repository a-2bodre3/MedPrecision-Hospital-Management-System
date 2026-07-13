using HospitalManagement.Domain.Value_Objects;
using HospitalManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace HospitalManagement.Application.Features.Structure.Branch.Commands.Create
{
    public class CreateBranchCommand : IRequest<bool>
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
        public required string PhoneNumber { get; set; }
        public required Address Address { get; set; }
    }
}
