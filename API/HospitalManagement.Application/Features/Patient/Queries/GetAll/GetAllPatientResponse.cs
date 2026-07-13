using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Patient.Queries.GetAll
{
    public record PatientsResponse
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string FullName { get; set; }
        public required string ImageUrl { get; set; }
        public required bool IsActive { get; set; }
        public required string PhoneNumber { get; set; }
        public required string PatientCode { get; set; }
    }
}
