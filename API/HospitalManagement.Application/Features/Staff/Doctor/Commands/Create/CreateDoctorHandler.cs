using AutoMapper;
using HospitalManagement.Domain.Entities.Staff;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Interfaces.Authentication;
using HospitalManagement.Domain.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using DoctorEntity = HospitalManagement.Domain.Entities.Staff.Doctor;

namespace HospitalManagement.Application.Features.Staff.Doctor.Commands.Create
{
    public class CreateDoctorHandler : IRequestHandler<CreateDoctorCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IPasswordHasher _passwordHasher;

        public CreateDoctorHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = _mapper.Map<DoctorEntity>(request);
            doctor.Employee.User.PasswordHash = _passwordHasher.HashPassword(request.Password);
            string imageUrl = "/uploads/default-avatar.png";
            if (request.ImageFile != null && request.ImageFile.Length > 0)
            {
                imageUrl = await _fileService.UploadFileAsync(request.ImageFile.OpenReadStream(), request.ImageFile.FileName, "images");
            }
            doctor.Employee.User.ImageUrl = imageUrl;
            doctor.Employee.User.IsActive = true;
            await _unitOfWork.Repository<DoctorEntity>().AddAsync(doctor);
            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
