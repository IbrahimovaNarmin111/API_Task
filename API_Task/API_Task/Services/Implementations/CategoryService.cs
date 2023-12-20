using API_Task.DTOs.CategoryDto;
using API_Task.Entities;
using API_Task.Repositories.Interfaces;
using API_Task.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace API_Task.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository repository,IMapper mapper) 
        { 
            _repository=repository;
            _mapper=mapper;
        }

      

        public async Task<IQueryable<Category>> GetAll()
        {
           return await _repository.GetAll(orderbyExpression:c=>c.Name,isDesting:true);
        }

        public Task<Category> GetById(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<Category> Create(CreateCategoryDto categorydto)
        {
           if(categorydto==null) throw new Exception();
            //Category category = new Category()
            //{
            //    Name = categorydto.Name,
            //    Description=categorydto.Description,
            //    Tag= categorydto.Tag,   
            //};
            Category category=_mapper.Map<Category>(categorydto);
            await _repository.Create(category);
            await _repository.SaveChangesAsync();
            return category;
        }
        public async Task<Category> Update(int id, UpdateCategoryDto categorydto)
        {
            if (categorydto == null) throw new Exception();
            var categories = await _repository.GetByIdAsync(id);
            if (categories == null) throw new Exception();
            categories.Name = categorydto.Name;
            categories.Description = categorydto.Description;
            categories.Tag=categorydto.Tag;
            _repository.Update(categories);
            await _repository.SaveChangesAsync();
            return categories;
        }

        //public void Delete(int id)
        //{
        //    if (id <= 0) throw new Exception();
        //    var categories =  _repository.GetByIdAsync(id);
        //    if (categories == null) throw new Exception();
        //    _repository.Delete(categories);
        //    _repository.SaveChangesAsync();
        //}
    }
}
