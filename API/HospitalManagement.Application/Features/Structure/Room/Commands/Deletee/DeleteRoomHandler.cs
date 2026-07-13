using HospitalManagement.Domain.Exceptions;
using HospitalManagement.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using RoomEntity = HospitalManagement.Domain.Entities.Structure.Room;

namespace HospitalManagement.Application.Features.Structure.Room.Commands.Deletee
{
    public class DeleteRoomHandler : IRequestHandler<DeleteRoomCommand , bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRoomHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await _unitOfWork.Repository<RoomEntity>().GetByIdAsync(request.Id , cancellationToken);
            if (room == null)
            {
                throw new NotFoundException("هذه الغرفه غير موجوده ");
            }
            _unitOfWork.Repository<RoomEntity>().Delete(room);
            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
