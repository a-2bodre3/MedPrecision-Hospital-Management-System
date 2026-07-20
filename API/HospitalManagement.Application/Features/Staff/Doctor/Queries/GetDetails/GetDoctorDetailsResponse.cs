using HospitalManagement.Application.Features.Staff.Employee.Queries.GetById;
using HospitalManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Staff.Doctor.Queries.GetDetails
{
    public record DoctorDetailsResponse : EmployeeDetailsResponse
    {
        public required int EmployeeId { get; set; }
        public required string LicenseNumber { get; set; }
        public required decimal ConsultationFee { get; set; }
        public required AcademicDegree Degree { get; set; }
        public required string Specialization { get; set; }
        public required string SubSpecialty { get; set; }
    }
}
