using AutoMapper;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Specifications.Room;
using RoomEntity = HospitalManagement.Domain.Entities.Structure.Room;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using HospitalManagement.Domain.Exceptions;

namespace HospitalManagement.Application.Features.Structure.Room.Queries.GetById
{
    public class GetRoomByIdHandler : IRequestHandler<RoomDetailsQuery, RoomDetailsResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetRoomByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<RoomDetailsResponse> Handle(RoomDetailsQuery request, CancellationToken cancellationToken)
        {
            var spec = new RoomDetailsSpec(request.Id);
            var room = await _unitOfWork.Repository<RoomEntity>().GetEntityWithSpecAsync(spec , cancellationToken);
            if(room == null)
            {
                throw new NotFoundException("هذه الغرفه غير موجوده");
            }
            return _mapper.Map<RoomDetailsResponse>(room);
        }
    }
}
