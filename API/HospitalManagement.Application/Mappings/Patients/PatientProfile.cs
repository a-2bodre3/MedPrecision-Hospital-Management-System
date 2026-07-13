using AutoMapper;
using HospitalManagement.Application.Features.Patient.Commands.Create;
using HospitalManagement.Application.Features.Patient.Commands.Update;
using HospitalManagement.Application.Features.Patient.Queries.GetAll;
using HospitalManagement.Application.Features.Patient.Queries.GetById;
using HospitalManagement.Domain.Entities.Patients;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Mappings.Patients
{
    internal class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, PatientsResponse>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src =>
                    src.User != null ? $"{src.User.FirstName} {src.User.LastName}" : string.Empty));

            CreateMap<Patient, PatientDetailsResponse>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src =>
                    src.User != null ? $"{src.User.FirstName} {src.User.LastName}" : string.Empty))
                .ForMember(dest => dest.Allergies, opt => opt.MapFrom(src =>
                    src.PatientAllergies.Select(a => a.Allergy.Name)))
                .ForMember(dest => dest.ChronicDiseases, opt => opt.MapFrom(src =>
                    src.PatientChronicDiseases.Select(d =>d.ChronicDisease.Name)));


            CreateMap<CreatePatientCommand, Patient>()
            .ForPath(dest => dest.User.ImageUrl, opt => opt.Ignore()) 
            .ForPath(dest => dest.User.PasswordHash, opt => opt.Ignore()) 
            .ForMember(dest => dest.PatientAllergies, opt => opt.Ignore()) 
            .ForMember(dest => dest.PatientChronicDiseases, opt => opt.Ignore());

            
            CreateMap<UpdatePatientCommand, Patient>()
                .ForPath(dest => dest.User.ImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.PatientAllergies, opt => opt.Ignore())
                .ForMember(dest => dest.PatientChronicDiseases, opt => opt.Ignore());
        }
    }
}
