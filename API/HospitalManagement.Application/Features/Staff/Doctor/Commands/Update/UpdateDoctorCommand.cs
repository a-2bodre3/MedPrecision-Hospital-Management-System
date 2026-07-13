using HospitalManagement.Application.Features.Staff.Employee.Commands.Update;
using HospitalManagement.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Staff.Doctor.Commands.Update
{
    public record UpdateDoctorCommand : UpdateEmployeeCommand , IRequest<bool>
    {
        public required decimal ConsultationFee { get; set; }
    }
}
