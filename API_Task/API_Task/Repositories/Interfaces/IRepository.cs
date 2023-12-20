using API_Task.Entities;
using API_Task.Entities.Base;
using System.Linq.Expressions;

namespace API_Task.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IQueryable<T>> GetAll(Expression<Func<T, bool>>? expression=null, Expression<Func<T, object>>? orderbyExpression = null, bool isDesting = false, params string[]? includes);
        Task<T> GetByIdAsync(int id, params string[]? includes); 
        Task Create(T entity);
        Task Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
    }
}
