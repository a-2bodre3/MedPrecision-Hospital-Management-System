using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Staff.Employee.Queries.GetById
{
    public record EmployeeDetailsQuery : IRequest<EmployeeDetailsResponse>
    {
        public int EmployeeId { get; set; }
    }
 
}
