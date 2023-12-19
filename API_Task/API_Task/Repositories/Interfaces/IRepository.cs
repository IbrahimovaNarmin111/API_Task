using API_Task.Entities;
using System.Linq.Expressions;

namespace API_Task.Repositories.Interfaces
{
    public interface IRepository
    {
        Task<IQueryable<Category>> GetAll(Expression<Func<Category, bool>>? expression=null, params string[]? includes);
        Task<Category> GetByIdAsync(int id, params string[]? includes); 
        Task Create(Category category);
        Task Update(Category category);
        void Delete(Category category);
        Task SaveChangesAsync();
    }
}
