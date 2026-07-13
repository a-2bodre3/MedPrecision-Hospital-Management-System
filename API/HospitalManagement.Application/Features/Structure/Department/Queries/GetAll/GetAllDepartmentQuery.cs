using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Department.Queries.GetAll
{
    public record DepartmentQuery : IRequest<List<DepartmentResponse>>;
}
