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
                    src.User != null ? $"{src.User.FirstName} {src.User.LastName}" : string.Empty))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User != null ? src.User.Email : string.Empty))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.User != null ? src.User.ImageUrl : string.Empty))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.User != null ? src.User.IsActive : false))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User != null ? src.User.PhoneNumber : string.Empty));



            CreateMap<Patient, PatientDetailsResponse>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src =>
                    src.User != null ? $"{src.User.FirstName} {src.User.LastName}" : string.Empty))
                .ForMember(dest => dest.Allergies, opt => opt.MapFrom(src =>
                    src.PatientAllergies.Select(a => a.Allergy.Name)))
                .ForMember(dest => dest.ChronicDiseases, opt => opt.MapFrom(src =>
                    src.PatientChronicDiseases.Select(d => d.ChronicDisease.Name)))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.User.IsActive))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.User.ImageUrl))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.User.CreatedAt))
                .ForPath(dest => dest.Address, opt => opt.MapFrom(src => src.Address));





            CreateMap<CreatePatientCommand, Patient>()
                .ForPath(dest => dest.User.Email, opt => opt.MapFrom(src => src.Email))
                .ForPath(dest => dest.User.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForPath(dest => dest.User.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForPath(dest => dest.User.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForPath(dest => dest.User.PasswordHash, opt => opt.Ignore())
                .ForPath(dest => dest.User.ImageUrl, opt => opt.Ignore())
                .ForPath(dest => dest.User.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.PatientCode, opt => opt.Ignore())
                .ForMember(dest => dest.PatientAllergies, opt =>
                     opt.MapFrom(src => src.Allergies != null
                        ? src.Allergies.Select(id => new PatientAllergy { AllergyId = id })
                        : new List<PatientAllergy>()))


                .ForMember(dest => dest.PatientChronicDiseases, opt =>
                    opt.MapFrom(src => src.ChronicDiseases != null
                        ? src.ChronicDiseases.Select(id => new PatientChronicDisease { ChronicDiseaseId = id })
                        : new List<PatientChronicDisease>()));




            CreateMap<UpdatePatientCommand, Patient>()
                .ForPath(dest => dest.User.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForPath(dest => dest.User.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForPath(dest => dest.User.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForPath(dest => dest.User.PasswordHash, opt => opt.Ignore())
                .ForPath(dest => dest.User.ImageUrl, opt => opt.Ignore())
                .ForPath(dest => dest.User.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.PatientCode, opt => opt.Ignore())
                .ForMember(dest => dest.PatientAllergies, opt =>
                     opt.MapFrom(src => src.Allergies != null
                        ? src.Allergies.Select(id => new PatientAllergy { AllergyId = id })
                        : new List<PatientAllergy>()))


                .ForMember(dest => dest.PatientChronicDiseases, opt =>
                    opt.MapFrom(src => src.ChronicDiseases != null
                        ? src.ChronicDiseases.Select(id => new PatientChronicDisease { ChronicDiseaseId = id })
                        : new List<PatientChronicDisease>()));
        }
    }
}
