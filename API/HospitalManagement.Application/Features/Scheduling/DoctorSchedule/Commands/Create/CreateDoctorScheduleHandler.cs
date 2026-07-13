using AutoMapper;
using HospitalManagement.Application.Interfaces;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Specifications.DoctorSchedule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using DoctorScheduleEntity = HospitalManagement.Domain.Entities.Scheduling.DoctorSchedule;

namespace HospitalManagement.Application.Features.Sheduling.DoctorSchedule.Commands.Create
{
    public class CreateDoctorScheduleHandler : IRequestHandler<CreateDoctorScheduleCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public CreateDoctorScheduleHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<bool> Handle(CreateDoctorScheduleCommand request, CancellationToken cancellationToken)
        {
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


            var doctorSchedule = _mapper.Map<DoctorScheduleEntity>(request);

            doctorSchedule.CreatedById = _currentUser.UserId;

            await repository.AddAsync(doctorSchedule, cancellationToken); 

            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

        }
    }
}
