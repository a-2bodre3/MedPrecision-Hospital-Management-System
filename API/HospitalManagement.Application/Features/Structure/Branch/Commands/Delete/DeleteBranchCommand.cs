using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Branch.Commands.Delete
{
    public class DeleteBranchCommand : IRequest<bool>
    {
        public required int Id { get; set; }
    }
}
