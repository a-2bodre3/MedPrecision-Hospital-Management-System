using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Branch.Commands.Delete
{
    public class DeleteBranchValidator : AbstractValidator<DeleteBranchCommand>
    {
        public DeleteBranchValidator()
        {
            RuleFor(b => b.Id)
                .GreaterThan(0).WithMessage("معرف الفرع غير صحيح");
        }
    }
}
