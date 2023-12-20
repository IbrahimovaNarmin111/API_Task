using API_Task.DAL;
using API_Task.Entities;
using API_Task.Repositories.Interfaces;

namespace API_Task.Repositories.Implementations
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext db) : base(db)
        {
        }
    }
}
