using API_Task.DAL;
using API_Task.DTOs.CategoryDto;
using API_Task.Entities;
using API_Task.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Task.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IRepository _repository;

        public CategoriesController(AppDbContext db,IRepository repository)
        {
            _db = db;
          _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _repository.GetAll();
            return Ok(data);

        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            var categories = await _db.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (categories == null) return StatusCode(StatusCodes.Status404NotFound);
            return StatusCode(StatusCodes.Status200OK, categories);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateCategoryDto categoryDto)
        {
            if (categoryDto == null) return StatusCode(StatusCodes.Status400BadRequest);
            Category category = new Category()
            {
                Name = categoryDto.Name,
            };
            await _repository.Create(category);
            await _repository.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created, category);
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id, string name)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            var categories = await _repository.GetByIdAsync(id);
            if (categories == null) return StatusCode(StatusCodes.Status404NotFound);
            categories.Name = name;
            _repository.Update(categories);
            await _repository.SaveChangesAsync();
            
            return StatusCode(StatusCodes.Status202Accepted, categories);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            var categories =await _repository.GetByIdAsync(id);
            if (categories == null) return StatusCode(StatusCodes.Status404NotFound);
            _repository.Delete(categories);
            await _repository.SaveChangesAsync();
            return Ok();
        }
    }
}
