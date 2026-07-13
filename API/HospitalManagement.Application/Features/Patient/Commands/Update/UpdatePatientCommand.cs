
using HospitalManagement.Domain.Enums;
using HospitalManagement.Domain.Value_Objects;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Patient.Commands.Update
{
    public record UpdatePatientCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public IFormFile? ImageFile { get; set; }
        public required Gender Gender { get; set; }
        public required Address Address { get; set; }
        public string? EmergencyPhoneNumber { get; set; }
        public string? EmergencyEmail { get; set; }
        public List<int>? Allergies { get; set; }
        public List<int>? ChronicDiseases { get; set; }
    }
}
