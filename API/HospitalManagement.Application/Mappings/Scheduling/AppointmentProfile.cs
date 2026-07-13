using AutoMapper;
using HospitalManagement.Application.DTO.Scheduling.Appointment;
using HospitalManagement.Domain.Entities.Scheduling;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Mappings.Scheduling
{
    internal class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, AppointmentDisplayDto>()
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src =>
                src.Patient != null && src.Patient.User != null
                    ? $"{src.Patient.User.FirstName} {src.Patient.User.LastName}"
                    : string.Empty))

            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src =>
                src.DoctorSchedule != null && src.DoctorSchedule.Doctor != null && src.DoctorSchedule.Doctor.Employee.User != null
                    ? $"{src.DoctorSchedule.Doctor.Employee.User.FirstName} {src.DoctorSchedule.Doctor.Employee.User.LastName}"
                    : string.Empty))

            .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src =>
                src.DoctorSchedule != null && src.DoctorSchedule.Room != null
                    ? src.DoctorSchedule.Room.RoomNumber
                    : string.Empty));


            CreateMap<AppointmentCreateDto, Appointment>()
            .ForMember(dest => dest.Price, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore()); 


            CreateMap<AppointmentUpdateDto, Appointment>();


            CreateMap<AppointmentStatusUpdateDto, Appointment>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status)); CreateMap<AppointmentCreateDto, Appointment>()
            .ForMember(dest => dest.Price, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore()); 

            CreateMap<AppointmentUpdateDto, Appointment>();

            CreateMap<AppointmentStatusUpdateDto, Appointment>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }
    }
}
