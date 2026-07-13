using AutoMapper;
using HospitalManagement.Application.Features.Staff.Doctor.Commands.Create;
using HospitalManagement.Application.Features.Staff.Doctor.Commands.Update;
using HospitalManagement.Application.Features.Staff.Doctor.Queries.GetDetails;
using HospitalManagement.Domain.Entities.Identity;
using HospitalManagement.Domain.Entities.Staff;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Mappings.Staff
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<Doctor, DoctorDetailsResponse>()

            .ForMember(dest => dest.Specialization, opt => opt.MapFrom(src =>
                src.SubSpecialty != null && src.SubSpecialty.Specialization != null
                    ? src.SubSpecialty.Specialization.Name
                    : string.Empty))
            .ForMember(dest => dest.SubSpecialty, opt => opt.MapFrom(src =>
                src.SubSpecialty != null ? src.SubSpecialty.Name : string.Empty))

            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src =>
                src.Employee != null && src.Employee.Department != null
                    ? src.Employee.Department.Name
                    : string.Empty))

            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src =>
                src.Employee != null && src.Employee.User != null && !string.IsNullOrEmpty(src.Employee.User.FirstName)
                    ? $"{src.Employee.User.FirstName} {src.Employee.User.LastName}"
                    : string.Empty))

            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src =>
                src.Employee != null &&
                src.Employee.User != null &&
                src.Employee.User.UserRoles != null &&
                src.Employee.User.UserRoles.Any()
                    ? src.Employee.User.UserRoles.Select(ur => ur.Role.Name).FirstOrDefault()
                    : string.Empty))

            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src =>
                src.Employee != null &&
                src.Employee.User != null &&
                src.Employee.User.UserBranches != null &&
                src.Employee.User.UserBranches.Any()
                    ? src.Employee.User.UserBranches
                        .Select(ub => ub.Branch.Name)
                        .FirstOrDefault()
                    : string.Empty));

            //TODO: Refactor to nested DTOs later
            CreateMap<CreateDoctorCommand, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore());

            CreateMap<CreateDoctorCommand, Employee>()
                .ForMember(dest => dest , opt => opt.MapFrom(src => src));

            CreateMap<CreateDoctorCommand, Doctor>()
                .ForMember(dest => dest.Employee, opt => opt.MapFrom(src => src));




            CreateMap<UpdateDoctorCommand, User>();
            CreateMap<UpdateDoctorCommand, Employee>();
            CreateMap<UpdateDoctorCommand, Doctor>()
                .ForMember(dest => dest.Employee.User.ImageUrl, opt => opt.Ignore());



        }
    }
}