using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Validators.Extensions
{
    internal static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T , string> IsValidEmail<T>(this IRuleBuilder<T,string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("البريد الإلكتروني مطلوب.")
                .EmailAddress().WithMessage("البريد الإلكتروني غير صحيح.");
        }
        public static IRuleBuilderOptions<T, string> IsValidPassword<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .IsValidPassword();
        }

    }
}
