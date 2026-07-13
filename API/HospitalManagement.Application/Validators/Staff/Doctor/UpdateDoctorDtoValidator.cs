using FluentValidation;
using HospitalManagement.Application.DTO.Staff;
using HospitalManagement.Application.DTO.Staff.Doctor;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Validators.Staff.Doctor
{
    public class UpdateDoctorDtoValidator : AbstractValidator<UpdateDoctorDto>
    {
        public UpdateDoctorDtoValidator()
        {
            Include(new StaffInfoModifyUpdateDtoValidator());

            RuleFor(d => d.ConsultationFee)
                .NotEmpty().WithMessage("سعر الكشف مطلوبة.");
        }
    }
}
