using HospitalManagement.Domain.Enums;
using HospitalManagement.Domain.Value_Objects;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Patient.Commands.Create
{
    public record CreatePatientCommand : IRequest<bool>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public IFormFile? ImageFile { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required Gender Gender { get; set; }
        public required Address Address { get; set; }
        public string? EmergencyPhoneNumber { get; set; }
        public string? EmergencyEmail { get; set; }
        public List<int>? Allergies { get; set; }
        public List<int>? ChronicDiseases { get; set; }
    }
}
