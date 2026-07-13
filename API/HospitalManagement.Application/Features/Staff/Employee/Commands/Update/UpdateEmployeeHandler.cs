using AutoMapper;
using HospitalManagement.Domain.Exceptions;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Interfaces.Authentication;
using HospitalManagement.Domain.Interfaces.Services;
using HospitalManagement.Domain.Specifications.Employee;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using EmployeeEntity = HospitalManagement.Domain.Entities.Staff.Employee;

namespace HospitalManagement.Application.Features.Staff.Employee.Commands.Update
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public UpdateEmployeeHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<bool> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var spec = new EmployeeDetailsSpec(request.Id);
            var employee = await _unitOfWork.Repository<EmployeeEntity>().GetEntityWithSpecAsync(spec, cancellationToken);

            if (employee == null)
            {
                throw new NotFoundException($"Employee with Id {request.Id} not found.");
            }

            _mapper.Map(request, employee);
            if (request.ImageFile != null && request.ImageFile.Length > 0)
            {
                string imageUrl = await _fileService.UploadFileAsync(request.ImageFile.OpenReadStream(), request.ImageFile.FileName, "images");
                employee.User.ImageUrl = imageUrl;
            }
            employee.User.IsActive = request.IsActive;
            _unitOfWork.Repository<EmployeeEntity>().Update(employee);

            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
