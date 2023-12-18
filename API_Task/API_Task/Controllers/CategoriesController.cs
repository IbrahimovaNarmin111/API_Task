using API_Task.DAL;
using API_Task.Entities;
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

        public CategoriesController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _db.Categories.ToListAsync();
            return Ok(categories);

        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            var categories = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (categories == null) return StatusCode(StatusCodes.Status404NotFound);
            return StatusCode(StatusCodes.Status200OK, categories);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (category == null) return StatusCode(StatusCodes.Status400BadRequest);
            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created, category);
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id, string name)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            var categories = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (categories == null) return StatusCode(StatusCodes.Status404NotFound);
            categories.Name = name;
            int result = await _db.SaveChangesAsync();
            if (result == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return StatusCode(StatusCodes.Status202Accepted, categories);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            var categories = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (categories == null) return StatusCode(StatusCodes.Status404NotFound);
            _db.Categories.Remove(categories);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
