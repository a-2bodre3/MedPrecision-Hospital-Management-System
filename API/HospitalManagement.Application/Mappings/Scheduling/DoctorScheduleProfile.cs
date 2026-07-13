using AutoMapper;
using HospitalManagement.Application.DTO.Scheduling.DoctorSchedule;
using HospitalManagement.Application.Features.Scheduling.DoctorSchedule.Commands.Update;
using HospitalManagement.Application.Features.Scheduling.DoctorSchedule.Queries.GetAllDoctorSchedules;
using HospitalManagement.Application.Features.Scheduling.DoctorSchedule.Queries.GetDoctorSchedules;
using HospitalManagement.Application.Features.Sheduling.DoctorSchedule.Commands.Create;
using HospitalManagement.Domain.Entities.Scheduling;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Mappings.Scheduling
{
    internal class DoctorScheduleProfile : Profile
    {
        public DoctorScheduleProfile()
        {

            CreateMap<CreateDoctorScheduleCommand, DoctorSchedule>()
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedById, opt => opt.Ignore());

            CreateMap<UpdateDoctorScheduleCommand, DoctorSchedule>()
                .ForMember(dest => dest.LastModifiedById, opt => opt.Ignore());

            CreateMap<DoctorSchedule, DoctorSchedulesResponse>()
                .ForMember(dest => dest.DoctorName , opt => opt.MapFrom(src =>
                    src.Doctor != null && src.Doctor.Employee.User != null
                    ? $"{src.Doctor.Employee.User.FirstName} {src.Doctor.Employee.User.LastName}"
                    : string.Empty))
                .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src =>
                src.Room != null ? src.Room.RoomNumber : string.Empty));

            CreateMap<DoctorSchedule , DoctorScheduleDetailsResponse>()
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src =>
                    src.Doctor != null && src.Doctor.Employee.User != null
                    ? $"{src.Doctor.Employee.User.FirstName} {src.Doctor.Employee.User.LastName}"
                    : string.Empty))
                .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src =>
                src.Room != null ? src.Room.RoomNumber : string.Empty));
        }
    }
}
