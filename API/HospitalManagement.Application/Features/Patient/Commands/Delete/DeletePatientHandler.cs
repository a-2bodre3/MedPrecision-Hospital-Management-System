using HospitalManagement.Domain.Entities.Staff;
using HospitalManagement.Domain.Exceptions;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Specifications.Patient;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using PatientEntity = HospitalManagement.Domain.Entities.Patients.Patient;

namespace HospitalManagement.Application.Features.Patient.Commands.Delete
{
    public class DeletePatientHandler : IRequestHandler<DeletePatientCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePatientHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            var spec = new PatientDetailsSpec(request.Id);
            var patient = await _unitOfWork.Repository<PatientEntity>().GetEntityWithSpecAsync(spec, cancellationToken);

            if (patient == null)
            {
                throw new NotFoundException($"Patient with Id {request.Id} not found.");
            }
            patient.User.IsActive = false;
            _unitOfWork.Repository<PatientEntity>().Update(patient);
            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
