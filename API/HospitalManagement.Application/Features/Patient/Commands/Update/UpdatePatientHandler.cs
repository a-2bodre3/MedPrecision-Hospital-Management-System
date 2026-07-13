using AutoMapper;
using HospitalManagement.Domain.Entities.Staff;
using HospitalManagement.Domain.Exceptions;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Interfaces.Services;
using HospitalManagement.Domain.Specifications.Patient;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using PatientEntity = HospitalManagement.Domain.Entities.Patients.Patient;

namespace HospitalManagement.Application.Features.Patient.Commands.Update
{
    public class UpdatePatientHandler : IRequestHandler<UpdatePatientCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public UpdatePatientHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<bool> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var spec = new PatientDetailsSpec(request.Id);
            var patient = await _unitOfWork.Repository<PatientEntity>().GetEntityWithSpecAsync(spec, cancellationToken);

            if (patient == null)
            {
                throw new NotFoundException($"Patient with Id {request.Id} not found.");
            }

            if (request.ImageFile != null && request.ImageFile.Length > 0)
            {
                string imageUrl = await _fileService.UploadFileAsync(request.ImageFile.OpenReadStream(), request.ImageFile.FileName, "images");
                patient.User.ImageUrl = imageUrl;
            }
            patient.User.IsActive = request.IsActive;
            _unitOfWork.Repository<PatientEntity>().Update(patient);

            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        }
    }
}
