using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Staff.Employee.Commands.Delete
{
    public record DeleteEmployeeCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
    
}
