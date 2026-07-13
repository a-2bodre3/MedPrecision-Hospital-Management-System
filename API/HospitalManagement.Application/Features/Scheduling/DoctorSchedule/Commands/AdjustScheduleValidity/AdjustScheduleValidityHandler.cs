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

namespace HospitalManagement.Application.Features.Scheduling.DoctorSchedule.Commands.AdjustScheduleValidity
{
    public class AdjustScheduleValidityHandler : IRequestHandler<AdjustScheduleValidityCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public AdjustScheduleValidityHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<bool> Handle(AdjustScheduleValidityCommand request, CancellationToken cancellationToken)
        {
            var doctorSchedule = await _unitOfWork.Repository<DoctorScheduleEntity>().GetByIdAsync(request.DoctorScheduleId, cancellationToken);
            if (doctorSchedule == null)
            {
                throw new NotFoundException("هذا الميعاد غير موجود");
            }
            if (request.ValidUntil.HasValue && request.ValidFrom >= request.ValidUntil.Value)
            {
                throw new BusinessRuleException("تاريخ بداية الصلاحية يجب أن يكون قبل تاريخ نهايتها.");
            }
            if (request.ValidUntil.HasValue && (doctorSchedule.ValidUntil == null || request.ValidUntil < doctorSchedule.ValidUntil))
            {
           
                var spec = new GetFutureAppointmentsAfterDateSpec(request.DoctorScheduleId, request.ValidUntil.Value);
                var hasOrphanedAppointments = await _unitOfWork.Repository<Appointment>().AnyAsync(spec);

                if (hasOrphanedAppointments)
                {
                    throw new BusinessRuleException("لا يمكن تقصير فترة صلاحية الجدول، يوجد مواعيد محجوزة للمرضى بعد التاريخ المحدد!");
                }
            }
            doctorSchedule.ValidFrom = request.ValidFrom;
            doctorSchedule.ValidUntil = request.ValidUntil;
            doctorSchedule.LastModifiedAt = DateTime.Now;
            doctorSchedule.LastModifiedById = _currentUser.UserId;
            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
            
        }
    }
}
