using FluentValidation;
using HospitalManagement.Application.Validators.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Identity.Users.ChangePassword
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0).WithMessage("ادخال معرف الموظف");

            RuleFor(p => p.Password)
                .IsValidPassword().WithMessage("كلمة المرور يجب أن تحتوي على 8 أحرف على الأقل، وحرف كبير واحد على الأقل， وحرف صغير واحد على الأقل، ورقم واحد على الأقل، ورمز خاص واحد على الأقل.");
        }
    }
}
