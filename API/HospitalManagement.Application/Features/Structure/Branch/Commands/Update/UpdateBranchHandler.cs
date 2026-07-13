using AutoMapper;
using HospitalManagement.Domain.Exceptions;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Specifications.Branches;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using BranchEntity = HospitalManagement.Domain.Entities.Structure.Branch;

namespace HospitalManagement.Application.Features.Structure.Branch.Commands.Update
{
    public class UpdateBranchHandler : IRequestHandler<UpdateBranchCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBranchHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
        {
            var spec = new BranchDetailsSpec(request.Id);
            var branch = await _unitOfWork.Repository<BranchEntity>().GetEntityWithSpecAsync(spec);

            if (branch == null) 
            {
                throw new NotFoundException("هذا الفرع غير موجود");
            }

            _mapper.Map(request, branch);

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
