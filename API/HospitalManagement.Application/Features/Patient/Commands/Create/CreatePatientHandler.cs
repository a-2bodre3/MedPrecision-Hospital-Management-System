using AutoMapper;
using HospitalManagement.Domain.Entities.Staff;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Interfaces.Authentication;
using HospitalManagement.Domain.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using PatientEntity = HospitalManagement.Domain.Entities.Patients.Patient;

namespace HospitalManagement.Application.Features.Patient.Commands.Create
{
    public class CreatePatientHandler : IRequestHandler<CreatePatientCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IPasswordHasher _passwordHasher;

        public CreatePatientHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = _mapper.Map<PatientEntity>(request);
            patient.User.PasswordHash = _passwordHasher.HashPassword(request.Password);
            string imageUrl = "/uploads/default-avatar.png";
            if (request.ImageFile != null && request.ImageFile.Length > 0)
            {
                imageUrl = await _fileService.UploadFileAsync(request.ImageFile.OpenReadStream(), request.ImageFile.FileName, "images");
            }
            patient.User.ImageUrl = imageUrl;
            patient.User.IsActive = true;
            
            var random = new Random();
            int number = random.Next(1000, 9999);
            patient.PatientCode = $"PT-{DateTime.Now:yyyyMMdd}-{number}";

            

            await _unitOfWork.Repository<PatientEntity>().AddAsync(patient);
            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
