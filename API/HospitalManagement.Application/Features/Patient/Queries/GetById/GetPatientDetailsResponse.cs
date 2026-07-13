using HospitalManagement.Domain.Entities.Common;
using HospitalManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Patient.Queries.GetById
{
    public record PatientDetailsResponse
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string FullName { get; set; }
        public required string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public required string PhoneNumber { get; set; }
        public required DateTime CreatedAt { get; set; }
        public DateTime DateOfBirth { get; set; }
        public required string PatientCode { get; set; }
        public required Gender Gender { get; set; }
        public string? EmergencyPhoneNumber { get; set; }
        public string? EmergencyEmail { get; set; }
        public required Address Address { get; set; }
        public List<string>? Allergies { get; set; }
        public List<string>? ChronicDiseases { get; set; }
    }
}
