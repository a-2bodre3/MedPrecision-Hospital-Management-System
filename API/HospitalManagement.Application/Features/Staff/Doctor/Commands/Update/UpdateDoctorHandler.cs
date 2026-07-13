using AutoMapper;
using HospitalManagement.Domain.Entities.Staff;
using HospitalManagement.Domain.Exceptions;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Interfaces.Services;
using HospitalManagement.Domain.Specifications.Doctor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using DoctorEntity = HospitalManagement.Domain.Entities.Staff.Doctor;

namespace HospitalManagement.Application.Features.Staff.Doctor.Commands.Update
{
    public class UpdateDoctorHandler : IRequestHandler<UpdateDoctorCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public UpdateDoctorHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<bool> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            var spec = new DoctorDetailsSpec(request.Id);
            var doctor = await _unitOfWork.Repository<DoctorEntity>().GetEntityWithSpecAsync(spec, cancellationToken);
            if (doctor == null)
            {
                throw new NotFoundException($"Doctor with Id {request.Id} not found.");
            }
            _mapper.Map(request, doctor);
            if (request.ImageFile != null && request.ImageFile.Length > 0)
            {
                string imageUrl = await _fileService.UploadFileAsync(request.ImageFile.OpenReadStream(), request.ImageFile.FileName, "images");
                doctor.Employee.User.ImageUrl = imageUrl;
            }
            doctor.Employee.User.IsActive = request.IsActive;
            _unitOfWork.Repository<DoctorEntity>().Update(doctor);
            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
