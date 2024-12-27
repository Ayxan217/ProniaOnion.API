using ProniaOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.Abstractions.Repositories.Generic
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        IQueryable<T> GetAll(
    Expression<Func<T, bool>>? expression = null,
    Expression<Func<T, object>>? orderExpression = null,
    int skip = 0,
    int take = 0,
    bool isDecending = false,
    bool isTracking = false,
    params string[]? includes);
        Task<T> GetByIdAsync(int id, params string[] includes);
        Task AddAsync(T entity);
        void Delete(T entity);
        void Update(T entity);
        Task<int> SaveChangesAsync();
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
    }
}
