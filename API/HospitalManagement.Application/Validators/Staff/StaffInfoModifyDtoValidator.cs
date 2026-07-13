using FluentValidation;
using HospitalManagement.Application.DTO.Staff;
using HospitalManagement.Application.Validators.Common;
using HospitalManagement.Application.Validators.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Validators.Staff
{
    public class StaffInfoModifyDtoValidator : AbstractValidator<StaffInfoModifyDto>
    {
        public StaffInfoModifyDtoValidator()
        {
            RuleFor(s => s.FirstName)
                .NotEmpty().WithMessage("الاسم الأول مطلوب.")
                .MaximumLength(50).WithMessage("الاسم الأول لا يجب أن يتخطى 50 حرف.");

            RuleFor(s => s.LastName)
                .NotEmpty().WithMessage("الاسم العائلة مطلوب.")
                .MaximumLength(50).WithMessage("الاسم العائلة لا يجب أن يتخطى 50 حرف.");

            RuleFor(s => s.PhoneNumber)
                  .NotEmpty().WithMessage("رقم الهاتف مطلوب.");


            RuleFor(s => s.RoleId)
                .NotEmpty().WithMessage("الدور مطلوب");

            RuleFor(s => s.BranchId)
                .NotEmpty().WithMessage("الفرع مطلوب");

            RuleFor(s => s.JobTitle)
                .NotEmpty().WithMessage("اسم الوظيفه مطلوب");

            RuleFor(s => s.DepartmentId)
                .NotEmpty().WithMessage("القسم مطلوب");

            RuleFor(s => s.Salary)
                .NotEmpty().WithMessage("راتب الموظف مطلوب");

            RuleFor(s => s.DateOfBirth)
                .NotEmpty().WithMessage("تاريخ الميلاد مطلوب");

            RuleFor(s => s.Gender)
                .NotEmpty().WithMessage("تحديد النوع مطلوب.");

            //RuleFor(s => s.Address)
            //    .SetValidator(new AddressValidator());
        }

    }

    public class StaffInfoModifyCreateDtoValidator : AbstractValidator<StaffInfoModifyCreateDto>
    {
        public StaffInfoModifyCreateDtoValidator()
        {
            Include(new StaffInfoModifyDtoValidator());
            RuleFor(p => p.Email)
                .IsValidEmail();

            RuleFor(p => p.Password)
                .IsValidPassword();
        }
    }

    public class StaffInfoModifyUpdateDtoValidator : AbstractValidator<StaffInfoModifyUpdateDto>
    {
        public StaffInfoModifyUpdateDtoValidator()
        {
            Include(new StaffInfoModifyDtoValidator());
            RuleFor(p => p.IsActive)
                .NotEmpty().WithMessage("يجب تحديد حاله الموظف");
        }
    }
}
