using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Application.DTO.Common
{
    public class PagedResult<T>
    {
        public required List<T> Items { get; init; }
        public int TotalCount { get; init; }
        public int PageNumber { get; init; }
        public int PageSize { get; init; }

        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
