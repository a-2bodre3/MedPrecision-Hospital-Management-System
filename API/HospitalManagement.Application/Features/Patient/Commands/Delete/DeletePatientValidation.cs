using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Patient.Commands.Delete
{
    public class DeletePatientValidation : AbstractValidator<DeletePatientCommand>
    {
        public DeletePatientValidation()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0).WithMessage("ادخال معرف الموظف");
        }
    }
}
