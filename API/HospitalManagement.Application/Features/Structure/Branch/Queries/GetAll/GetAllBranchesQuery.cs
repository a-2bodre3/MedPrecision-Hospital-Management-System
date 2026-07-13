using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Branch.Queries.GetAll
{
    public record GetAllBranchesQuery : IRequest<List<BranchResponse>>;
}
