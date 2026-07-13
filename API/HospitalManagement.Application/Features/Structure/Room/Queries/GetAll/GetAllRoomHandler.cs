using AutoMapper;
using RoomEntity = HospitalManagement.Domain.Entities.Structure.Room;
using HospitalManagement.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Room.Queries.GetAll
{
    public class GetAllRoomHandler : IRequestHandler<RoomQuery, List<RoomResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllRoomHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<RoomResponse>> Handle(RoomQuery request, CancellationToken cancellationToken)
        {
            var rooms = await _unitOfWork.Repository<RoomEntity>().ListAllAsync(cancellationToken);
            return _mapper.Map<List<RoomResponse>>(rooms);
        }
    }
}
