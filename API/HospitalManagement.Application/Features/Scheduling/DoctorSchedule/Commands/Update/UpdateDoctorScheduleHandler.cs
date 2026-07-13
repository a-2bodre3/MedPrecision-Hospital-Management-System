using AutoMapper;
using HospitalManagement.Application.Interfaces;
using HospitalManagement.Domain.Exceptions;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Specifications.DoctorSchedule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using DoctorScheduleEntity = HospitalManagement.Domain.Entities.Scheduling.DoctorSchedule;

namespace HospitalManagement.Application.Features.Scheduling.DoctorSchedule.Commands.Update
{
    public class UpdateDoctorScheduleHandler : IRequestHandler<UpdateDoctorScheduleCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public UpdateDoctorScheduleHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<bool> Handle(UpdateDoctorScheduleCommand request, CancellationToken cancellationToken)
        {
            var doctorSchedule = await _unitOfWork.Repository<DoctorScheduleEntity>().GetByIdAsync(request.Id , cancellationToken);
            if(doctorSchedule == null)
            {
                throw new NotFoundException("هذا الميعاد غير موجود");
            }

            var repository = _unitOfWork.Repository<DoctorScheduleEntity>();

            var roomSpec = new RoomScheduleOverlapSpec(request.RoomId, request.StartTime, request.EndTime, request.DayOfWeeks);
            var isRoomOccupied = await repository.AnyAsync(roomSpec, cancellationToken);

            if (isRoomOccupied)
            {
                throw new Exception("هذه الغرفة محجوزة لطبيب آخر في بعض الأيام المختارة ونفس الوقت!");
            }

            var doctorSpec = new DoctorScheduleOverlapSpec(request.DoctorId, request.StartTime, request.EndTime, request.DayOfWeeks);
            var isDoctorBusy = await repository.AnyAsync(doctorSpec, cancellationToken);

            if (isDoctorBusy)
            {
                throw new Exception("هذا الطبيب لديه شيفت آخر في نفس الأيام والوقت المختار!");
            }

            doctorSchedule.LastModifiedAt = DateTime.Now;
            doctorSchedule.LastModifiedById = _currentUser.UserId;

            _mapper.Map(request , doctorSchedule);
            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
            
        }
    }
}
