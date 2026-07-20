using AutoMapper;
using HospitalManagement.Application.DTO.Common;
using HospitalManagement.Application.Features.Staff.Doctor.Commands.Create;
using HospitalManagement.Application.Features.Staff.Doctor.Commands.Update;
using HospitalManagement.Application.Features.Staff.Doctor.Queries.GetAll;
using HospitalManagement.Application.Features.Staff.Doctor.Queries.GetDetails;
using HospitalManagement.Domain.Entities.Identity;
using HospitalManagement.Domain.Entities.Staff;
using HospitalManagement.Domain.Value_Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Mappings.Staff
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<Doctor, DoctorsResponse>()
                .ForPath(dest => dest.FullName, opt => opt.MapFrom(src =>
                    src.Employee.User != null ? $"{src.Employee.User.FirstName} {src.Employee.User.LastName}" : string.Empty))

                .ForPath(dest => dest.DepartmentName, opt => opt.MapFrom(src =>
                    src.Employee.Department != null ? src.Employee.Department.Name : string.Empty))

                .ForPath(dest => dest.Email, opt => opt.MapFrom(src =>
                    src.Employee.User != null ? src.Employee.User.Email : string.Empty))
                .ForPath(dest => dest.ImageUrl, opt => opt.MapFrom(src =>
                    src.Employee.User != null ? src.Employee.User.ImageUrl : string.Empty))
                .ForPath(dest => dest.IsActive, opt => opt.MapFrom(src =>
                    src.Employee.User != null ? src.Employee.User.IsActive : false));


            CreateMap<Doctor, DoctorDetailsResponse>()
                .ForPath(dest => dest.Email, opt => opt.MapFrom(src =>
                    src.Employee.User != null ? src.Employee.User.Email : string.Empty))
                .ForPath(dest => dest.ImageUrl, opt => opt.MapFrom(src =>
                    src.Employee.User != null ? src.Employee.User.ImageUrl : string.Empty))
                .ForPath(dest => dest.IsActive, opt => opt.MapFrom(src =>
                    src.Employee.User != null ? src.Employee.User.IsActive : false))
                .ForPath(dest => dest.PhoneNumber, opt => opt.MapFrom(src =>
                    src.Employee.User != null ? src.Employee.User.PhoneNumber : string.Empty))
                .ForPath(dest => dest.EmployeeId , opt => opt.MapFrom(src => src.EmployeeId))
                .ForPath(dest => dest.JobTitle, opt => opt.MapFrom(src =>
                    src.Employee.User != null ? src.Employee.JobTitle : string.Empty))
                .ForPath(dest => dest.HireDate, opt => opt.MapFrom(src =>
                    src.Employee.User != null ? src.Employee.HireDate : (DateTime?)null))
                .ForPath(dest => dest.BirthDate, opt => opt.MapFrom(src =>
                    src.Employee.User != null ? src.Employee.DateOfBirth : (DateTime?)null))
                .ForPath(dest => dest.Salary, opt => opt.MapFrom(src =>
                    src.Employee.User != null ? src.Employee.Salary : 0))

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

            .ForPath(dest => dest.BranchName, opt => opt.MapFrom(src =>
                src.Employee.User != null && src.Employee.User.UserBranches != null && src.Employee.User.UserBranches.Any()
                    ? string.Join(", ", src.Employee.User.UserBranches.Select(ub => ub.Branch.Name))
                    : string.Empty))

            .ForPath(dest => dest.RoleName, opt => opt.MapFrom(src =>
                src.Employee.User != null && src.Employee.User.UserRoles != null && src.Employee.User.UserRoles.Any()
                    ? string.Join(", ", src.Employee.User.UserRoles.Select(ub => ub.Role.Name))
                    : string.Empty))

            .ForPath(dest => dest.Address, opt => opt.MapFrom(src => src.Employee.Address));




            CreateMap<CreateDoctorCommand, Doctor>()
                .ForPath(dest => dest.Employee.User.Email, opt => opt.MapFrom(src => src.Email))
                .ForPath(dest => dest.Employee.User.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForPath(dest => dest.Employee.User.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForPath(dest => dest.Employee.JobTitle, opt => opt.MapFrom(src => src.JobTitle))
                .ForPath(dest => dest.Employee.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
                .ForPath(dest => dest.Employee.User.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForPath(dest => dest.Employee.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForPath(dest => dest.Employee.Salary, opt => opt.MapFrom(src => src.Salary))

                .ForPath(dest => dest.Employee.User.PasswordHash, opt => opt.Ignore())
                .ForPath(dest => dest.Employee.User.ImageUrl, opt => opt.Ignore())
                .ForPath(dest => dest.Employee.User.IsActive, opt => opt.Ignore())
                .ForPath(dest => dest.Employee.User.UserBranches, opt => opt.MapFrom(src =>
                    src.BranchId > 0
                        ? new List<UserBranch> { new UserBranch { BranchId = src.BranchId, UserId = 0 } }
                        : new List<UserBranch>()))

                .ForPath(dest => dest.Employee.User.UserRoles, opt => opt.MapFrom(src =>
                    src.RoleId > 0
                        ? new List<UserRole> { new UserRole { RoleId = src.RoleId, UserId = 0 } }
                        : new List<UserRole>()))

                .ForPath(dest => dest.Employee.Address, opt => opt.MapFrom(src => new Address
                {
                    Street = src.Address.Street,
                    City = src.Address.City,
                    Country = src.Address.Country,

                }));



            CreateMap<UpdateDoctorCommand, Doctor>()
                .ForPath(dest => dest.Employee.User.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForPath(dest => dest.Employee.User.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForPath(dest => dest.Employee.JobTitle, opt => opt.MapFrom(src => src.JobTitle))
                .ForPath(dest => dest.Employee.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
                .ForPath(dest => dest.Employee.User.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForPath(dest => dest.Employee.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForPath(dest => dest.Employee.Salary, opt => opt.MapFrom(src => src.Salary))

                .ForPath(dest => dest.Employee.User.PasswordHash, opt => opt.Ignore())
                .ForPath(dest => dest.Employee.User.ImageUrl, opt => opt.Ignore())
                .ForPath(dest => dest.Employee.User.IsActive, opt => opt.Ignore())
                .ForPath(dest => dest.Employee.User.UserBranches, opt => opt.MapFrom(src =>
                    src.BranchId > 0
                        ? new List<UserBranch> { new UserBranch { BranchId = src.BranchId, UserId = 0 } }
                        : new List<UserBranch>()))

                .ForPath(dest => dest.Employee.User.UserRoles, opt => opt.MapFrom(src =>
                    src.RoleId > 0
                        ? new List<UserRole> { new UserRole { RoleId = src.RoleId, UserId = 0 } }
                        : new List<UserRole>()))

                .ForPath(dest => dest.Employee.Address, opt => opt.MapFrom(src => new Address
                {
                    Street = src.Address.Street,
                    City = src.Address.City,
                    Country = src.Address.Country,

                }));




        }
    }
}