using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(string includeProperties);
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> filter, string includeProperties = "");
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> filter, string[] includeProperties);
        Task<T> GetById(object id);

        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(int id);
        Task Delete(T entity);
    }
}
