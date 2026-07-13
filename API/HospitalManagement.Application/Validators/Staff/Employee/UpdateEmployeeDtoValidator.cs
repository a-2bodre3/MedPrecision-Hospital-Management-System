using FluentValidation;
using HospitalManagement.Application.DTO.Staff.Employee;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Validators.Staff.Employee
{
    public class UpdateEmployeeDtoValidator : AbstractValidator<UpdateEmployeeDto>
    {
        public UpdateEmployeeDtoValidator()
        {
            Include(new StaffInfoModifyUpdateDtoValidator());
        }
    }
}
