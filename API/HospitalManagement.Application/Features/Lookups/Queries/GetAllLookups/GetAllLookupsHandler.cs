using HospitalManagement.Domain.Entities.Identity;
using HospitalManagement.Domain.Entities.Patients;
using HospitalManagement.Domain.Entities.Staff;
using HospitalManagement.Domain.Entities.Structure;
using HospitalManagement.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Features.Lookups.Queries.GetAppLookups
{
    public class GetAllLookupsHandler : IRequestHandler<LookupsQuery , AppLookupsResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllLookupsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AppLookupsResponse> Handle(LookupsQuery request, CancellationToken cancellationToken)
        {
            var branches = await _unitOfWork.Repository<Branch>().ListAllAsync(cancellationToken);
            var departments = await _unitOfWork.Repository<Department>().ListAllAsync(cancellationToken);
            var rooms = await _unitOfWork.Repository<Room>().ListAllAsync(cancellationToken);
            var roles = await _unitOfWork.Repository<Role>().ListAllAsync(cancellationToken);
            var specializations = await _unitOfWork.Repository<Specialization>().ListAllAsync(cancellationToken);
            var subSpecialty = await _unitOfWork.Repository<SubSpecialty>().ListAllAsync(cancellationToken);
            var allergies = await _unitOfWork.Repository<Allergy>().ListAllAsync(cancellationToken);
            var chronicDisease = await _unitOfWork.Repository<ChronicDisease>().ListAllAsync(cancellationToken);



            return new AppLookupsResponse
            {
                Branches = branches.Select(b => new LookupDto(b.Id, b.Name)).ToList(),
                Departments = departments.Select(b => new LookupDto(b.Id, b.Name)).ToList(),
                Rooms = rooms.Select(b => new LookupDto(b.Id, b.RoomNumber)).ToList(),
                Roles = roles.Select(r => new LookupDto(r.Id, r.Name)).ToList(),
                Specialization = specializations.Select(s => new LookupDto(s.Id , s.Name)).ToList(),
                SubSpecialty = subSpecialty.Select(ss => new LookupDto(ss.Id , ss.Name)).ToList(),
                Allergies = allergies.Select(a => new LookupDto(a.Id , a.Name)).ToList(),
                ChronicDisease = chronicDisease.Select(cd => new LookupDto(cd.Id , cd.Name)).ToList(),

            };
        }
    }
}
