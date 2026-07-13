using HospitalManagement.Application.DTO.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.Interfaces
{
    public interface IPaginationService
    {
        Task<PagedResult<T>> CreatePagedListAsync<T>(
                IQueryable<T> source,
                int pageNumber,
                int pageSize,
                CancellationToken cancellationToken = default
            );
      
    }
}
