using HospitalManagement.Application.Features.Staff.Employee.Commands.Create;
using HospitalManagement.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Staff.Doctor.Commands.Create
{
    public record CreateDoctorCommand : CreateEmployeeCommand , IRequest<bool>
    {
        public required string LicenseNumber { get; set; }
        public required decimal ConsultationFee { get; set; }
        public required AcademicDegree Degree { get; set; }
        public required int SubSpecialtyId { get; set; }
    }
}
