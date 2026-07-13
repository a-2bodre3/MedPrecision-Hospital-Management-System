using AutoMapper;
using HospitalManagement.Domain.Exceptions;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Specifications.Room;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using RoomEntity = HospitalManagement.Domain.Entities.Structure.Room;

namespace HospitalManagement.Application.Features.Structure.Room.Commands.Update
{
    public class UpdateRoomHandler : IRequestHandler<UpdateRoomCommand , bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateRoomHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            var spec = new RoomDetailsSpec(request.Id);
            var room = await _unitOfWork.Repository<RoomEntity>().GetEntityWithSpecAsync(spec, cancellationToken);

            if (room == null)
            {
                throw new NotFoundException("هذه الغرفه غير موجوده ");
            }
            _mapper.Map(request, room);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
