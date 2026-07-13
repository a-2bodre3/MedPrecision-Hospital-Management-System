using HospitalManagement.Application.Interfaces;
using HospitalManagement.Domain.Entities.Scheduling;
using HospitalManagement.Domain.Exceptions;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Specifications.DoctorSchedule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using DoctorScheduleEntity = HospitalManagement.Domain.Entities.Scheduling.DoctorSchedule;

namespace HospitalManagement.Application.Features.Scheduling.DoctorSchedule.Commands.Delete
{
    public class DeleteDoctorScheduleHandler : IRequestHandler<DeleteDoctorScheduleCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public DeleteDoctorScheduleHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<bool> Handle(DeleteDoctorScheduleCommand request, CancellationToken cancellationToken)
        {
            var doctorSchedule = await _unitOfWork.Repository<DoctorScheduleEntity>().GetByIdAsync(request.Id);
            if (doctorSchedule == null) 
            {
                throw new NotFoundException("هذا الميعاد غير موجود");
            }
            var spec = new GetFutureAppointmentsByScheduleSpec(request.Id, DateTime.Now);
            var hasFutureAppointments = await _unitOfWork.Repository<Appointment>().AnyAsync(spec);

            if (hasFutureAppointments)
            {
                throw new BusinessRuleException("لا يمكن حذف هذا الجدول، يوجد مواعيد محجوزة لهذا الطبيب!");
            }

            doctorSchedule.IsActive = false;
            doctorSchedule.ValidUntil = DateTime.Now;
            doctorSchedule.DeletedAt = DateTime.Now;
            doctorSchedule.IsDeleted = true;
            doctorSchedule.DeletedById = _currentUser.UserId;
            _unitOfWork.Repository<DoctorScheduleEntity>().Update(doctorSchedule);
            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
