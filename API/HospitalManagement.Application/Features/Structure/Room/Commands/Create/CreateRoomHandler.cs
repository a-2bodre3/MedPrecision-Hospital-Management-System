using AutoMapper;
using HospitalManagement.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using RoomEntity = HospitalManagement.Domain.Entities.Structure.Room;

namespace HospitalManagement.Application.Features.Structure.Room.Commands.Create
{
    internal class CreateRoomHandler : IRequestHandler<CreateRoomCommand , bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateRoomHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var room = _mapper.Map<RoomEntity>(request);
            room.IsActive = true;

            await _unitOfWork.Repository<RoomEntity>().AddAsync(room , cancellationToken);
            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
