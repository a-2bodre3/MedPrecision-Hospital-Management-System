using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.DTO.Common
{
    public record QueryParameters
    {
        const int maxPageSize = 50;
        private int _pageSize = 10;

        public int PageNumber { get; init; } = 1;

        public int PageSize
        {
            get => _pageSize;
            init => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }
}
