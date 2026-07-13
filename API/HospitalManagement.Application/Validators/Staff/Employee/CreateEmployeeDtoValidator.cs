using FluentValidation;
using HospitalManagement.Application.DTO.Staff.Employee;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Validators.Staff.Employee
{
    public class CreateEmployeeDtoValidator : AbstractValidator<CreateEmployeeDto>
    {
        public CreateEmployeeDtoValidator()
        {
            Include(new StaffInfoModifyCreateDtoValidator());
        }
    }
}
