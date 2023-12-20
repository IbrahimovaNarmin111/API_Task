using API_Task.DTOs.CategoryDto;
using API_Task.Entities;

namespace API_Task.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IQueryable<Category>> GetAll();
        Task<Category> GetById(int id);
        Task<Category> Create(CreateCategoryDto categorydto);
        Task<Category> Update(int id,UpdateCategoryDto categorydto);
        //void Delete(int id);
    }
}
