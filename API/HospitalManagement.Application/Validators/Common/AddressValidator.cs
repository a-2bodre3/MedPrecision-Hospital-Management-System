using FluentValidation;
using HospitalManagement.Application.DTO.Common;
using HospitalManagement.Domain.Value_Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Validators.Common
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(p => p.Country)
                 .NotEmpty().WithMessage("الدولة مطلوبة.");

            RuleFor(p => p.City)
                .NotEmpty().WithMessage("المدينه مطلوبة.");

            RuleFor(p => p.Street)
                .NotEmpty().WithMessage("الشارع مطلوبة.");
        }
    }
}
