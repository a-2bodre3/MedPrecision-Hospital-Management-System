using BranchEntity = HospitalManagement.Domain.Entities.Structure.Branch;
using AutoMapper;
using HospitalManagement.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using HospitalManagement.Domain.Specifications.Branches;

namespace HospitalManagement.Application.Features.Structure.Branch.Queries.GetAll
{
    public class GetAllBranchesHandler : IRequestHandler<GetAllBranchesQuery , List<BranchResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllBranchesHandler(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<BranchResponse>> Handle(GetAllBranchesQuery request, CancellationToken token)
        {
            var branches = await _unitOfWork.Repository<BranchEntity>().ListAllAsync();

            return _mapper.Map<List<BranchResponse>>(branches);
        }
    }
}
