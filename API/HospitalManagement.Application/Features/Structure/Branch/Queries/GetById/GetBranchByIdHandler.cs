using AutoMapper;
using HospitalManagement.Domain.Exceptions;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Specifications.Branches;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using BranchEntity = HospitalManagement.Domain.Entities.Structure.Branch;

namespace HospitalManagement.Application.Features.Structure.Branch.Queries.GetById
{
    public class GetBranchByIdHandler : IRequestHandler<GetBranchByIdQuery, BranchDetailsResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetBranchByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BranchDetailsResponse> Handle(GetBranchByIdQuery request , CancellationToken token)
        {
            var spec = new BranchDetailsSpec(request.Id);

            var branch = await _unitOfWork.Repository<BranchEntity>().GetEntityWithSpecAsync(spec);

            if (branch == null) 
            {
                throw new NotFoundException("هذا الفرع غير موجود");
            }

            return _mapper.Map<BranchDetailsResponse>(branch);
        }
    }
}
