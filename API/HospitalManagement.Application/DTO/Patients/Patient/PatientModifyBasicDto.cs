using HospitalManagement.Application.DTO.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.DTO.Patients.Patient
{
    public abstract record PatientModifyBasicDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public required IFormFile ImageFile { get; set; }
        public string? EmergencyPhoneNumber { get; set; }
        public string? EmergencyEmail { get; set; }
        public List<int>? Allergies { get; set; }
        public List<int>? ChronicDiseases { get; set; }
        public required string Gender { get; set; }
        public required AddressDto Address { get; set; }
    }
}
