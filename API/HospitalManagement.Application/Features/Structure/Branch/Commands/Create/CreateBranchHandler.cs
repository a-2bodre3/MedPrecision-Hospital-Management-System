using AutoMapper;
using BranchEntity = HospitalManagement.Domain.Entities.Structure.Branch;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Structure.Branch.Commands.Create
{
    public class CreateBranchHandler : IRequestHandler<CreateBranchCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBranchHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
        {
            var branch = _mapper.Map<BranchEntity>(request);
            branch.IsActive = true;
            branch.Address.AddressType =AddressType.BranchLocation;

            await _unitOfWork.Repository<BranchEntity>().AddAsync(branch);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
