using API_Task.DAL;
using API_Task.Entities;
using API_Task.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace API_Task.Repositories.Implementations
{
    public class Repository : IRepository
    {
        private AppDbContext _db;
        public Repository(AppDbContext db)
        {
            _db = db;
        }

       

        public async Task<IQueryable<Category>> GetAll(Expression<Func<Category,bool>>?expression=null,params string[]? includes)
        {
          IQueryable<Category> query  =  _db.Categories;
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
            return query;
        }

        public async Task<Category> GetByIdAsync(int id, params string[]? includes)
        {
            IQueryable<Category> data =  _db.Categories.AsNoTracking();
            if (includes is not null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                   data = data.Include(includes[i]);
                }
            }
            return await data.FirstOrDefaultAsync(c=>c.Id==id);
        }
        public async Task Create(Category category)
        {
            await _db.Categories.AddAsync(category);
        }

        public void Delete(Category category)
        {
            _db.Categories.Remove(category);
        }
        public async Task Update(Category category)
        {
            _db.Categories.Update(category);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();   
        }
    }
}
