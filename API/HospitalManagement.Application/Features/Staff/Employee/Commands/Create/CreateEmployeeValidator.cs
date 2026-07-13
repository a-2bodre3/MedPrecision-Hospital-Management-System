using FluentValidation;
using HospitalManagement.Application.Validators.Common;
using HospitalManagement.Application.Validators.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Staff.Employee.Commands.Create
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(e => e.Email)
                .IsValidEmail();
            RuleFor(e => e.Password)
                .IsValidPassword();
            RuleFor(e => e.FirstName)

                .NotEmpty().WithMessage("الاسم الأول مطلوب.")
                .MaximumLength(50).WithMessage("الاسم الأول لا يجب أن يتخطى 50 حرف.");

            RuleFor(e => e.LastName)
                .NotEmpty().WithMessage("الاسم العائلة مطلوب.")
                .MaximumLength(50).WithMessage("الاسم العائلة لا يجب أن يتخطى 50 حرف.");

            RuleFor(e => e.PhoneNumber)
                  .NotEmpty().WithMessage("رقم الهاتف مطلوب.");


            RuleFor(e => e.RoleId)
                .NotEmpty().WithMessage("الدور مطلوب");

            RuleFor(e => e.BranchId)
                .NotEmpty().WithMessage("الفرع مطلوب");

            RuleFor(e => e.JobTitle)
                .NotEmpty().WithMessage("اسم الوظيفه مطلوب");

            RuleFor(e => e.DepartmentId)
                .NotEmpty().WithMessage("القسم مطلوب");

            RuleFor(e => e.Salary)
                .NotEmpty().WithMessage("راتب الموظف مطلوب")
                .InclusiveBetween(1000m , 20000m).WithMessage("الراتب يجب أن يكون بين 1000 و 20000");

            RuleFor(e => e.DateOfBirth)
                .NotEmpty().WithMessage("تاريخ الميلاد مطلوب");

            RuleFor(e => e.Gender)
                .NotEmpty().WithMessage("تحديد النوع مطلوب.");
            RuleFor(e => e.Address)
                .SetValidator(new AddressValidator());
        }
    }
}
