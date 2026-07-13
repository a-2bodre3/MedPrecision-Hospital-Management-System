using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Branch.Queries.GetById
{
    public record GetBranchByIdQuery : IRequest<BranchDetailsResponse>
    {
        public int Id { get; set; }
    }
}
