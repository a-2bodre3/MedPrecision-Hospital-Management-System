using AutoMapper;

using HospitalManagement.Application.Features.Staff.Employee.Commands.Create;
using HospitalManagement.Application.Features.Staff.Employee.Commands.Update;
using HospitalManagement.Application.Features.Staff.Employee.Queries.GetAll;
using HospitalManagement.Application.Features.Staff.Employee.Queries.GetById;
using HospitalManagement.Application.Features.Structure.Branch.Commands.Create;
using HospitalManagement.Domain.Entities.Identity;
using HospitalManagement.Domain.Entities.Staff;
using System.Linq;

namespace HospitalManagement.Application.Mappings.Staff;

internal class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {

        CreateMap<Employee, EmployeeResponse>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src =>
                src.User != null ? $"{src.User.FirstName} {src.User.LastName}" : string.Empty))

            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src =>
                src.Department != null ? src.Department.Name : string.Empty))


            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User != null ? src.User.Email : string.Empty))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.User != null ? src.User.ImageUrl : string.Empty))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.User != null && src.User.IsActive));



        CreateMap<Employee, EmployeeDetailsResponse>()

            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User != null ? src.User.PhoneNumber : string.Empty))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.User != null ? src.DateOfBirth : (DateTime?)null))
            .ForPath(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForPath(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.User.ImageUrl))

            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src =>
                src.User != null && src.User.UserBranches != null && src.User.UserBranches.Any()
                    ? string.Join(", ", src.User.UserBranches.Select(ub => ub.Branch.Name))
                    : string.Empty))

            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src =>
                src.User != null && src.User.UserRoles != null && src.User.UserRoles.Any()
                    ? string.Join(", ", src.User.UserRoles.Select(ur => ur.Role.Name))
                    : string.Empty))

            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src =>
                src.User != null ? $"{src.User.FirstName} {src.User.LastName}" : string.Empty))

            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.User != null && src.User.IsActive));



        CreateMap<CreateEmployeeCommand, Employee>()
             .ForPath(dest => dest.User.Email, opt => opt.MapFrom(src => src.Email))
             .ForPath(dest => dest.User.FirstName, opt => opt.MapFrom(src => src.FirstName))
             .ForPath(dest => dest.User.LastName, opt => opt.MapFrom(src => src.LastName))
             .ForPath(dest => dest.User.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
             .ForPath(dest => dest.User.PasswordHash, opt => opt.Ignore())
             .ForPath(dest => dest.User.ImageUrl, opt => opt.Ignore())
             .ForPath(dest => dest.User.IsActive, opt => opt.Ignore())
            .ForPath(dest => dest.User.UserBranches, opt => opt.MapFrom(src =>
                src.BranchId > 0
                    ? new List<UserBranch> { new UserBranch { BranchId = src.BranchId, UserId = 0 } }
                    : new List<UserBranch>()))

            .ForPath(dest => dest.User.UserRoles, opt => opt.MapFrom(src =>
                src.RoleId > 0
                    ? new List<UserRole> { new UserRole { RoleId = src.RoleId, UserId = 0 } }
                    : new List<UserRole>()));



        CreateMap<UpdateEmployeeCommand, Employee>()
             .ForPath(dest => dest.User.FirstName, opt => opt.MapFrom(src => src.FirstName))
             .ForPath(dest => dest.User.LastName, opt => opt.MapFrom(src => src.LastName))
             .ForPath(dest => dest.User.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
             .ForPath(dest => dest.User.PasswordHash, opt => opt.Ignore())
             .ForPath(dest => dest.User.ImageUrl, opt => opt.Ignore())
             .ForPath(dest => dest.User.IsActive, opt => opt.Ignore())
             .ForPath(dest => dest.User.UserBranches, opt => opt.MapFrom(src =>
                src.BranchId > 0
                    ? new List<UserBranch> { new UserBranch { BranchId = src.BranchId, UserId = 0 } }
                    : new List<UserBranch>()))

            .ForPath(dest => dest.User.UserRoles, opt => opt.MapFrom(src =>
                src.RoleId > 0
                    ? new List<UserRole> { new UserRole { RoleId = src.RoleId, UserId = 0 } }
                    : new List<UserRole>()));



    }
}