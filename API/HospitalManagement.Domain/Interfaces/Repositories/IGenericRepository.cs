using HospitalManagement.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<T>> ListAllAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default);

        Task<T?> GetEntityWithSpecAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
        IQueryable<T> GetQueryWithSpec(ISpecification<T> spec);

        Task<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);

        Task<bool> AnyAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);

        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        void Update(T entity);
        void Delete(T entity);
    }
}
