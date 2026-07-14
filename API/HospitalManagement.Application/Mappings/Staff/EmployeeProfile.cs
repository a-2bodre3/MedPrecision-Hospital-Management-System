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
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.User != null ? src.User.ImageUrl : string.Empty));



        CreateMap<Employee, EmployeeDetailsResponse>()

            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User != null ? src.User.PhoneNumber : string.Empty))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.User != null ? src.DateOfBirth : (DateTime?)null))


            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src =>
                src.User.UserBranches != null && src.User.UserBranches.Any()
                    ? string.Join(", ", src.User.UserBranches.Select(ub => ub.Branch.Name))
                    : string.Empty))

            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src =>
                src.User.UserRoles != null && src.User.UserRoles.Any()
                    ? string.Join(", ", src.User.UserRoles.Select(ur => ur.Role.Name))
                    : string.Empty));


        CreateMap<CreateEmployeeCommand, Employee>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src))
             .ForMember(dest => dest.User.Email, opt => opt.MapFrom(src => src.Email))
             .ForMember(dest => dest.User.FirstName, opt => opt.MapFrom(src => src.FirstName))
             .ForMember(dest => dest.User.LastName, opt => opt.MapFrom(src => src.LastName))
             .ForMember(dest => dest.User.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
             .ForMember(dest => dest.User.PasswordHash, opt => opt.Ignore())
             .ForMember(dest => dest.User.ImageUrl, opt => opt.Ignore())
             .ForMember(dest => dest.User.IsActive, opt => opt.Ignore());

        

        CreateMap<UpdateEmployeeCommand, Employee>()
             .ForMember(dest => dest.User, opt => opt.MapFrom(src => src))
             .ForMember(dest => dest.User.FirstName, opt => opt.MapFrom(src => src.FirstName))
             .ForMember(dest => dest.User.LastName, opt => opt.MapFrom(src => src.LastName))
             .ForMember(dest => dest.User.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
             .ForMember(dest => dest.User.PasswordHash, opt => opt.Ignore())
             .ForMember(dest => dest.User.ImageUrl, opt => opt.Ignore())
             .ForMember(dest => dest.User.IsActive, opt => opt.Ignore());

       

    }
}