using AutoMapper;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Interfaces.Authentication;
using HospitalManagement.Domain.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using EmployeeEntity = HospitalManagement.Domain.Entities.Staff.Employee;

namespace HospitalManagement.Application.Features.Staff.Employee.Commands.Create
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IPasswordHasher _passwordHasher;

        public CreateEmployeeHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = _mapper.Map<EmployeeEntity>(request);

            employee.User.PasswordHash = _passwordHasher.HashPassword(request.Password);
            string imageUrl = "/uploads/default-avatar.png";
            if(request.ImageFile !=null && request.ImageFile.Length > 0)
            {
                imageUrl = await _fileService.UploadFileAsync(request.ImageFile.OpenReadStream(), request.ImageFile.FileName,"images");
            }
            employee.User.ImageUrl = imageUrl;
            employee.User.IsActive = true;

            await _unitOfWork.Repository<EmployeeEntity>().AddAsync(employee);
            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
