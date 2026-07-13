using AutoMapper;
using HospitalManagement.Domain.Exceptions;
using HospitalManagement.Domain.Interfaces;
using HospitalManagement.Domain.Specifications.Branches;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using BranchEntity = HospitalManagement.Domain.Entities.Structure.Branch;

namespace HospitalManagement.Application.Features.Structure.Branch.Commands.Delete
{
    public class DeleteBranchHandler : IRequestHandler<DeleteBranchCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteBranchHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
        {
            var spec = new BranchDetailsSpec(request.Id);
            var branch = await _unitOfWork.Repository<BranchEntity>().GetByIdAsync(request.Id);
            if (branch == null)
            {
                throw new NotFoundException("هذا الفرع غير موجود");
            }
            if (!branch.IsActive)
            {
                throw new BadRequestException("هذا الفرع غير مفعل بالفعل");
            }

            branch.IsActive = false;

            return await _unitOfWork.SaveChangesAsync() > 0;

            
        }
    }
}
