using API_Task.DAL;
using API_Task.Entities;
using API_Task.Entities.Base;
using API_Task.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace API_Task.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private AppDbContext _db;
        private DbSet<T> _table;
        public Repository(AppDbContext db)
        {
            _db = db;
            _table=db.Set<T>();
        }

       

        public async Task<IQueryable<T>> GetAll(Expression<Func<T,bool>>?expression=null, Expression<Func<T, object>>? orderbyExpression = null,bool isDesting=false,params string[]? includes)
        {
          IQueryable<T> query  =  _table;
            if(expression is not null)
            {
                query = query.Where(expression);
            }
            if(includes is not null)
            {
                for(int i=0;i<includes.Length;i++)
                {
                    query=query.Include(includes[i]);
                }
            }
            if(orderbyExpression !=null)
            {
               query=isDesting? query.OrderByDescending(orderbyExpression):query.OrderBy(orderbyExpression);
            }
            return query;
        }

        public async Task<T> GetByIdAsync(int id, params string[]? includes)
        {
            IQueryable<T> data =  _table;
            if (includes is not null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                   data = data.Include(includes[i]);
                }
            }
            return await data.FirstOrDefaultAsync(c=>c.Id==id);
        }
        public async Task Create(T entity)
        {
            await _table.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }
        public async Task Update(T entity)
        {
            _table.Update(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();   
        }
    }
}
